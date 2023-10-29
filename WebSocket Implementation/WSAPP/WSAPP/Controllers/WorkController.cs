using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace WSAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        [HttpPost]
        [Route("server/ping")]
        public async Task<string> PingAsync()
        {
            string text = "";
            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri("ws://localhost:6767/message?id=2"), CancellationToken.None);
                var buffer = new byte[256];
                while (ws.State == WebSocketState.Open || ws.State == WebSocketState.CloseSent)
                {
                    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

                    Console.WriteLine(result.ToString());
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        return text;
                    }
                    else
                    {
                        text = Encoding.ASCII.GetString(buffer, 0, result.Count);
                        return text;
                    }
                }
                return text;
            }
        }

        [HttpPost]
        [Route("work/start")]
        public async Task<string> WorkAsync()
        {
            string text = "";
            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri("ws://localhost:6767/message?id=3"), CancellationToken.None);
                var buffer = new byte[256];
                while (ws.State == WebSocketState.Open || ws.State == WebSocketState.CloseSent)
                {
                    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

                    Console.WriteLine(result.ToString());
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        return text;
                    }
                    else
                    {
                        text = Encoding.ASCII.GetString(buffer, 0, result.Count);
                        return text;
                    }
                }
                return text;
            }
        }
    }
}
