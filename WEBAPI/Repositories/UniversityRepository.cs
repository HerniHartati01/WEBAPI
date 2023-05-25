using Microsoft.EntityFrameworkCore;
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
        public University CreateWithValidate(University university)
        {
            try
            {
                var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
                var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

                if (existingUniversityWithCode != null & existingUniversityWithName != null)
                {
                    university.Guid = existingUniversityWithCode.Guid;

                    _context.SaveChanges();

                }
                else
                {
                    _context.Universities.Add(university);
                    _context.SaveChanges();
                }

                return university;

            }
            catch
            {
                return null;
            }
        }
    }

    
}
