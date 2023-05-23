using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class UniversityRepository : RepositoryGeneric<University>, IUniversityRepository
    {
        public UniversityRepository(BookingMangementDbContext context) : base(context) { }
        public IEnumerable<University> GetByName(string name)
        {
            return _context.Set<University>().Where(u => u.Name.Contains(name)).ToList();
        }
    }
}
