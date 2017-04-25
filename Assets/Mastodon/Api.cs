using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using WebSocketSharp;

namespace Mastodon
{
    public partial class Api
    {
        private readonly Instance _instance;

        public Api(Instance instance)
        {
            _instance = instance;
        }

        public IObservable<string> Post(string path, Query query)
        {
            var baseUri = "https://" + _instance.Hostname;
            return ObservableWWW.Post(baseUri + path, query.GetWWWForm());
        }

        public WebSocket Connect(string path, Query query)
        {
            var baseUri = "wss://" + _instance.Hostname;
            return new WebSocket(baseUri + path + "?" + query.GetQueryString());
        }

        public class Query : Dictionary<string, string>
        {
            public WWWForm GetWWWForm()
            {
                var form = new WWWForm();
                foreach (var kv in this)
                {
                    form.AddField(kv.Key, kv.Value);
                }
                return form;
            }

            public string GetQueryString()
            {
                return this
                    .Select(x => string.Format("{0}={1}", WWW.EscapeURL(x.Key), WWW.EscapeURL(x.Value)))
                    .Aggregate((x, next) => x + "&" + next);
            }
        }
    }
}