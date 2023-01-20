using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealPromo.API.Notifications;
using RealPromo.API.Repository;
using RealPromo.API.ViewModels;
using System.Threading.Tasks;

namespace RealPromo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : MainController
    {
        private readonly IClientRepository _clientRepository;
        public ClientController(INotificador notificador, IClientRepository clientRepository) : base(notificador)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("clients")]
        public async Task<ActionResult> Cliente()
        {
            return CustomResponse(await _clientRepository.Clients());

        }
    }
}
