using RealPromo.API.ViewModels;
using System.Threading.Tasks;

namespace RealPromo.API.Service
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> GerarJwt(string email, string password);
        Task<UserTokenViewModel> GetUser(string email, string password);
    }
}
