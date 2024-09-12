using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces
{
    public interface IChatService
    {
        Task HandleWebSocketAsync(WebSocket webSocket, string username);
    }
}
