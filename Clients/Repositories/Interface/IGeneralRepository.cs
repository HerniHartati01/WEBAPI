using Clients.ViewModels.Others;

namespace Clients.Repositories.Interface
{
    public interface IGeneralRepository<TEntity, T>
        where TEntity : class
    {
        Task<ResponseListVM<TEntity>> Get();
        Task<ResponseVM<TEntity>> Get(T id);
        Task<ResponseMessageVM> Post(TEntity entity);
        Task<ResponseMessageVM> Put(TEntity entity);
        Task<ResponseMessageVM> Delete(T id);
    }
}
