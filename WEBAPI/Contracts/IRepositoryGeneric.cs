namespace WEBAPI.Contracts
{
    public interface IRepositoryGeneric<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetByGuid(Guid guid);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(Guid guid);
      

    }
}
