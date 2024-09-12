using ChatApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class ChatService : IChatService
    {
        private static readonly List<WebSocket> _connectedClients = new List<WebSocket>();

        public async Task HandleWebSocketAsync(WebSocket webSocket, string username)
        {
            _connectedClients.Add(webSocket);
            var buffer = new byte[1024 * 4];

            // Принимаем и отправляем сообщения
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _connectedClients.Remove(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the client", CancellationToken.None);

                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var formattedMessage = $"{username}: {message}";
                    await BroadcastMessageAsync(formattedMessage);
                }
            }
        }
        private async Task BroadcastMessageAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var client in _connectedClients)
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }

        }
    }
}
