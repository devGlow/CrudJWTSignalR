using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Projet101.Hubs;
using Projet101.Services.Auth;

namespace Projet101.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController(IAppAuthentication authentication, Microsoft.AspNetCore.SignalR.IHubContext<NotificationHub, INotification> hubContext) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(UserDto request)
        {

            var token = await authentication.LoginAsync(request);

            if (token is null)
                return BadRequest(new { erreur = "Utilisateur ou mot de passe incorrect." });


            return Ok(token);
        }


        [HttpPost("send-to-group")]
        public async Task<IActionResult> SendMessageToaGroupe()
        {
            await hubContext.Clients.All.ReceiveMessage($"Youpi!! Message Received from API this time is a send to groupe all  at {DateTime.Now} !!");

            var message = new { Titre = "This is a test", Message = $"The test has been sent on {DateTime.Now}." };


            return Ok(new { Message = $"Notification sent to a groupe" });

        }

    }
}
