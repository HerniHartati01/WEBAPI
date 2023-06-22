using Clients.Models;
using Clients.ViewModels.Others;

namespace Clients.Repositories.Interface
{
    public interface IAccountRepository : IGeneralRepository<Account, String>
    {
        public Task<ResponseViewModel<string>> LoginClient(LoginVM entity);
        public Task<ResponseMessageVM> RegisterClient(RegisterVM entity);
    }
}
