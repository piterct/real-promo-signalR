using RealPromo.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealPromo.API.Repository
{
    public interface IClientRepository
    {
        Task<List<ClientsViewModel>> Clients();
    }
}
