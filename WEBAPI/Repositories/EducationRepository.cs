using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class EducationRepository : RepositoryGeneric<Education>, IEducationRepository
    {
        public EducationRepository(BookingMangementDbContext context) : base(context) { }

        public IEnumerable<Education> GetByUniversityId(Guid universityId)
        {
            return _context.Set<Education>().Where(e => e.UniversityGuid == universityId); 
        }

        public Education GetByEmployeeId(Guid employeeId)
        {
            return _context.Set<Education>().Find(employeeId);
        }
    }
}
