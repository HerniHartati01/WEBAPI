using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IUniversityRepository : IRepositoryGeneric<University>
    {
        
        IEnumerable<University> GetByName(string name);
        
    }
}
