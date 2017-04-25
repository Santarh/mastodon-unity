using System;
using UniRx;
using WebSocketSharp;

namespace Mastodon
{
    public class StreamingApiConnection : IDisposable
    {
        public readonly IObservable<StreamingEvent> EventsAsObservable;

        public readonly IObservable<Status> StatusesAsObservable;

        private readonly WebSocket _socket;

        public StreamingApiConnection(WebSocket socket)
        {
            _socket = socket;
            EventsAsObservable = _socket.MessageAsObservable()
                .Select(x => new StreamingEvent(x.Data));
            StatusesAsObservable = EventsAsObservable
                .Where(x => x.Type == "update")
                .Select(x => new Status(x.Payload));
            _socket.ConnectAsync();
        }

        public void Dispose()
        {
            if (_socket != null) ((IDisposable) _socket).Dispose();
        }
    }
}