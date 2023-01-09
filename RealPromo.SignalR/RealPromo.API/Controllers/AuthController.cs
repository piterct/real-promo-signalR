using RealPromo.API.Notifications;

namespace RealPromo.API.Controllers
{
    public class AuthController : MainController
    {
        public AuthController(INotificador notificador) : base(notificador)
        {
        }
    }
}
