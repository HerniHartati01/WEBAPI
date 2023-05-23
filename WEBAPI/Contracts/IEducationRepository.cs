using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IEducationRepository : IRepositoryGeneric<Education>
    {
        IEnumerable<Education> GetByUniversityId(Guid universityId);
    }
}
