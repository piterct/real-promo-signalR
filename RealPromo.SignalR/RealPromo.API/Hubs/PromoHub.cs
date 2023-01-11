using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealPromo.API.Models;
using System.Threading.Tasks;

namespace RealPromo.API.Hubs
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PromoHub : Hub
    {
        public async Task CadastrarPromocao(Promocao promocao)
        {
            await Clients.Caller.SendAsync("CadastradoSucesso");
            await Clients.Others.SendAsync("ReceberPromocao", promocao);
        }
    }
}
