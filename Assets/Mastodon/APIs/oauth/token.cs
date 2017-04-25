using System.Collections;
using System.Collections.Generic;
using LitJson;
using UniRx;
using UnityEngine;

namespace Mastodon
{
    public partial class Api
    {
        public IObservable<string> GetAccessToken(string clientId, string clientSecret, string userName, string password)
        {
            return Post("/oauth/token", new Query
                {
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"grant_type", "password"},
                    {"username", userName},
                    {"password", password},
                }).Select(JsonMapper.ToObject)
                .Select(x => x.GetStringOrElse("access_token"));
        }
    }
}
