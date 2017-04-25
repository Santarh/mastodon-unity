# mastodon-unity

Streaming API の受信だけ。

## Usage

```csharp
var api = new Api(new Instance("friends.nico"));
api.CreateOAuthApp("YourOAuthAppName")
    .SelectMany(x => api.GetAccessToken(x.Id, x.Secret, "yourname@example.com", "password"))
    .Subscribe(token =>
    {
        // 本当は token はアプリの起動終了を跨いでキャッシュしてね。
        var connection = api.ConnectStreamingApi(token, Api.StreamType.LocalTimeline);
        connection.StatusesAsObservable.Subscribe(x => Debug.Log(x.ContentHtml)).AddTo(this);
        // 本当は Dispose してね。
        // connection.Dispose();
    });
```

## Dependencies

### UniRx
https://github.com/neuecc/UniRx

### LitJSON
https://github.com/lbv/litjson

### WebSocket-Sharp
https://github.com/sta/websocket-sharp
