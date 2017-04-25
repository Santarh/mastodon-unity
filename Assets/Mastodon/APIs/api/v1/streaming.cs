using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using LitJson;
using UnityEngine;

namespace Mastodon
{
    public partial class Api
    {
        public StreamingApiConnection ConnectStreamingApi(string accessToken, StreamType type)
        {
            return new StreamingApiConnection(Connect("/api/v1/streaming", new Query
            {
                { "access_token", accessToken },
                { "stream", StreamTypeToString(type) },
            }));
        }

        private string StreamTypeToString(StreamType type)
        {
            // https://github.com/tootsuite/mastodon/blob/master/streaming/index.js#L307
            switch (type)
            {
                case StreamType.User:
                    return "user";
                case StreamType.FederatedTimeline:
                    return "public";
                case StreamType.LocalTimeline:
                    return "public:local";
                case StreamType.FederatedHashtag:
                    return "hashtag";
                case StreamType.LocalHashtag:
                    return "hashtag:local";
                default:
                    throw new InvalidEnumArgumentException(type.ToString());
            }
        }

        public enum StreamType
        {
            User,
            FederatedTimeline,
            LocalTimeline,
            FederatedHashtag,
            LocalHashtag,
        }
    }
}
