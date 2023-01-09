using RealPromo.API.Notifications;
using RealPromo.API.Service;

namespace RealPromo.API.Controllers
{
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;
        public AuthController(INotificador notificador, IAuthService authService) : base(notificador)
        {
            _authService = authService;
        }
    }
}
