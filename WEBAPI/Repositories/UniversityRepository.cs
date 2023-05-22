using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class UniversityRepository : IRepositoryGeneric<University>
    {
        private readonly BookingMangementDbContext _context;
        public UniversityRepository(BookingMangementDbContext context)
        {
            _context = context;
        }
        
        public University Create(University university)
        {
            try
            {
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;
            }
            catch
            {
                return new University();
            }
        }

       
        public bool Update(University university)
        {
            try
            {
                _context.Set<University>().Update(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

       
        public bool Delete(Guid guid)
        {
            try
            {
                var university = GetByGuid(guid);
                if (university == null)
                {
                    return false;
                }

                _context.Set<University>().Remove(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();
        }

        
        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>().Find(guid);
        }
    }
}
