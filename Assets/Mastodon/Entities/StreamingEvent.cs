using LitJson;

namespace Mastodon
{
    public class StreamingEvent
    {
        public readonly string Type;
        public readonly string Payload;

        public StreamingEvent(string json)
        {
            var data = JsonMapper.ToObject(json);
            Type = data["event"].ToString();
            Payload = data["payload"].ToString();
        }
    }
}