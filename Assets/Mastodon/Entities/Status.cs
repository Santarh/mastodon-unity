using LitJson;

namespace Mastodon
{
    /// <summary>
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#status
    /// </summary>
    public class Status
    {
        public readonly string Id;
        public readonly string Uri;
        public readonly string Url;
        public readonly Account Account;
        public readonly string ContentHtml;

        public Status(string json) : this(JsonMapper.ToObject(json))
        {

        }

        public Status(JsonData data)
        {
            if (!data.IsObject) return;

            Id = data.GetStringOrElse("id");
            Uri = data.GetStringOrElse("uri");
            Url = data.GetStringOrElse("url");
            Account = new Account(data["account"]);
            ContentHtml = data.GetStringOrElse("content");
        }
    }
}