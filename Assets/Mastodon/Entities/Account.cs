using LitJson;

namespace Mastodon
{
    /// <summary>
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#account
    /// </summary>
    public class Account
    {
        public readonly string Id;
        public readonly string Username;
        public readonly string Url;
        public readonly string AvatarUrl;

        public Account(JsonData data)
        {
            if (!data.IsObject) return;

            Id = data.GetStringOrElse("id");
            Username = data.GetStringOrElse("username");
            Url = data.GetStringOrElse("url");
            AvatarUrl = data.GetStringOrElse("avatar");
        }
    }
}