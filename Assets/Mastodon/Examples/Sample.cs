using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UniRx;

namespace Mastodon
{
    public class Sample : MonoBehaviour
    {
        public string UserName = "your_email@example.com";
        public string Password = "password";
        private const string ApplicationName = "MastodonUnity";

        private const string AccessTokenKey = "NicoFriendsAccessToken";

        private StreamingApiConnection _connection;

        void Start()
        {
            var api = new Api(new Instance("friends.nico"));
            if (PlayerPrefs.HasKey(AccessTokenKey))
            {
                Debug.Log("Use a key already exists");
                ReceiveStreaming(api, PlayerPrefs.GetString(AccessTokenKey));
            }
            else
            {
                api.CreateOAuthApp(ApplicationName)
                    .SelectMany(x => api.GetAccessToken(x.Id, x.Secret, UserName, Password))
                    .Subscribe(x =>
                    {
                        PlayerPrefs.SetString(AccessTokenKey, x);
                        PlayerPrefs.Save();
                        ReceiveStreaming(api, x);
                    });
            }
        }

        private void ReceiveStreaming(Api api, string token)
        {
            _connection = api.ConnectStreamingApi(token, Api.StreamType.LocalTimeline);

            _connection.StatusesAsObservable
                .Subscribe(ProcessStatus, Debug.LogError, () => { })
                .AddTo(this);
        }

        private void ProcessStatus(Status s)
        {
            var username = s.Account.Username;
            var text = Regex.Replace(s.ContentHtml, @"<.*?>", string.Empty);

            Debug.Log(username + ": " + text);
        }

        private void OnDestroy()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
