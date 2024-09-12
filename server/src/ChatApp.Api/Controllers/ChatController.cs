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
        private readonly ITokenService _tokenService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService, ITokenService tokenService)
        {
            _logger = logger;
            _chatService = chatService;
            _tokenService = tokenService;
        }

        [HttpGet("/ws")]
        public async Task<IActionResult> WebSocketEndpoint(string token)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                return BadRequest("WebSocket connection expected.");
            }

            var claimsPrincipal = _tokenService.ValidateToken(token);
            if (claimsPrincipal == null)
            {
                return Unauthorized("Invalid or expired JWT token.");
            }

            var username = claimsPrincipal.Identity.Name;
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _chatService.HandleWebSocketAsync(webSocket, username);

            return Ok();
        }

    }
}
