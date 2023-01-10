using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealPromo.API.Notifications;
using RealPromo.API.Service;
using RealPromo.API.ViewModels;
using System.Threading.Tasks;

namespace RealPromo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;
        public AuthController(INotificador notificador, IAuthService authService) : base(notificador)
        {
            _authService = authService;
        }

        [HttpPost("autentica")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.GetUser(loginUser.Email, loginUser.Password);

            if (result != null)
            {
                return CustomResponse(await _authService.GerarJwt(loginUser.Email, loginUser.Password));
            }

            NotificarErro("username or password is not valid.");
            return CustomResponse(loginUser);
        }
    }
}
