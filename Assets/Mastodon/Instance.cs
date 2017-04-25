namespace Mastodon
{
    public class Instance
    {
        public readonly string Hostname;

        public Instance(string hostname = "friends.nico")
        {
            Hostname = hostname;
        }
    }
}
