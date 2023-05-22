using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IRepositoryGeneric
    {
        University Create(University university);
        bool Update(University university);
        bool Delete(Guid guid);
        IEnumerable<University> GetAll();
        University? GetByGuid(Guid guid);
    }
}
