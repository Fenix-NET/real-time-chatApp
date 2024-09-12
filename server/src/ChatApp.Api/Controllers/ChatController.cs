using ChatApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/v1/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }
        [HttpGet("/ws")]
        public async Task<IActionResult> WebSocketEndpoint(string token)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                return BadRequest("WebSocket connection expected.");
            }

        }

    }
}
