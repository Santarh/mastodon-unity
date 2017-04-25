using System;
using UniRx;
using WebSocketSharp;

namespace Mastodon
{
    public static class WebSocketSharpExtensions
    {
        public static IObservable<EventArgs> OpenAsObservable(this WebSocket socket)
        {
            return Observable.FromEvent<EventHandler, EventArgs>(
                (x) => (sender, e) => x(e),
                (x) => socket.OnOpen += x,
                (x) => socket.OnOpen -= x
            );
        }

        public static IObservable<CloseEventArgs> CloseAsObservable(this WebSocket socket)
        {
            return Observable.FromEvent<EventHandler<CloseEventArgs>, CloseEventArgs>(
                x => (sender, e) => x(e), 
                x => socket.OnClose += x,
                x => socket.OnClose -= x
            );
        }

        public static IObservable<ErrorEventArgs> ErrorAsObservable(this WebSocket socket)
        {
            return Observable.FromEvent<EventHandler<ErrorEventArgs>, ErrorEventArgs>(
                x => (sender, e) => x(e), 
                x => socket.OnError += x,
                x => socket.OnError -= x
            );
        }

        public static IObservable<MessageEventArgs> MessageAsObservable(this WebSocket socket)
        {
            return Observable.FromEvent<EventHandler<MessageEventArgs>, MessageEventArgs>(
                x => (sender, e) => x(e), 
                x => socket.OnMessage += x,
                x => socket.OnMessage -= x
            );
        }
    }
}