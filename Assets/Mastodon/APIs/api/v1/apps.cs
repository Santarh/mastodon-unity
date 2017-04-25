using System.Collections;
using System.Collections.Generic;
using LitJson;
using UniRx;
using UnityEngine;

namespace Mastodon
{
    public partial class Api
    {
        public IObservable<OAuthClient> CreateOAuthApp(string appName, string scopes = "read")
        {
            return Post("/api/v1/apps", new Query
                {
                    {"client_name", appName },
                    { "redirect_uris", "urn:ietf:wg:oauth:2.0:oob" },
                    { "scopes", scopes },
                })
                .Select(x =>
                {
                    var json = JsonMapper.ToObject(x);
                    return new OAuthClient
                    {
                        Id = json["client_id"].ToString(),
                        Secret = json["client_secret"].ToString(),
                    };
                });
        }
    }
}
