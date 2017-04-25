using LitJson;

namespace Mastodon
{
    public static class LitJsonExtensions
    {
        public static string GetStringOrElse(this JsonData data, string key)
        {
            if (!data.IsObject || !data.Keys.Contains(key))
            {
                return "";
            }
            return data[key].ToString();
        }
    }
}