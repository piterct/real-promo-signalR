using RealPromo.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealPromo.API.Repository
{
    public class ClientRepository : IClientRepository
    {
        public async Task<List<ClientsViewModel>> Clients()
        {
            return await Task.FromResult(
                new List<ClientsViewModel>() { new ClientsViewModel { Id = Guid.NewGuid(), Name = "Jhon", MiddleName = "Appex" },
                { new ClientsViewModel { Id = Guid.NewGuid(), Name = "July", MiddleName = "Manx" } } });
        }
    }
}
