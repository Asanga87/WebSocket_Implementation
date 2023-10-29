using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using WSAPP.Logics;
using WSAPP.Services;

namespace WSAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {

        private readonly IMessageBuilderService _messageBuilder; // Interface for Message bulder class. This generate the server repose message.
        List<WebSocket> WebSockets = new List<WebSocket>();// This websocket list use th send messages multiple clients.
        public WebSocketController(IMessageBuilderService messageBuilder)
        {
            _messageBuilder = messageBuilder;

        }

       //Implementation of Single web socket end point.
        [HttpGet("/message")]
        public async Task Get(int id)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                WebSockets.Add(webSocket);

                if(id==1)
                { 
               
                    await welcomeMethod(webSocket); 
                }

               else if (id==2)
                { 
                    await pingMethod(webSocket); 
                
                }

                else if (id == 3)
                {
                    await workMethod(webSocket);

                }

            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
      
        }
      
        //Implementation of Welcome call. Send message from websocket to client.
        private  async Task welcomeMethod(WebSocket webSocket)
        {
            var WellcomeMessage=_messageBuilder.PrepareMesage("Wellcome", 0);
            await webSocket.SendAsync((ArraySegment<byte>)WellcomeMessage, WebSocketMessageType.Text, true, CancellationToken.None);

           
        }
        //Implementation of Welcome call. Send message from websocket to post method.
        private async Task pingMethod(WebSocket webSocket)
        {
            var PingMessage = _messageBuilder.PrepareMesage("Pong", 0);
            foreach (var sockets in WebSockets)

             {
                await webSocket.SendAsync((ArraySegment<byte>)PingMessage, WebSocketMessageType.Text, true, CancellationToken.None);

              }
           

        }
        //Implementation of Welcome call. Send message from websocket to post method.
        private async Task workMethod(WebSocket webSocket)
        {

            var WorkStartedMessage = _messageBuilder.PrepareMesage("WorkStarted", 1);
            var WorkCompletedMessage = _messageBuilder.PrepareMesage("WorkCompleted", 2);
            await webSocket.SendAsync((ArraySegment<byte>)WorkStartedMessage, WebSocketMessageType.Text, true, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open)
            {
                //Setting delay
                await Task.Delay(3000);
                //Sending Last Message
                await webSocket.SendAsync((ArraySegment<byte>)WorkCompletedMessage, WebSocketMessageType.Text, true, CancellationToken.None);
                //Close Session
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "ServerClose", new CancellationTokenSource(20_000).Token);

               
            }

       
        }

     


    }
}
