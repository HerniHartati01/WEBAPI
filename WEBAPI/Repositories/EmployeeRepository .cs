using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;

namespace WEBAPI.Repositories
{
    public class EmployeeRepository : IRepositoryGeneric<Employee>
    {
        private readonly BookingMangementDbContext _context;
        public EmployeeRepository(BookingMangementDbContext context)
        {
            _context = context;
        }

        public Employee Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return new Employee();
            }
        }


        public bool Update(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Update(employee);
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
                var employee = GetByGuid(guid);
                if (employee == null)
                {
                    return false;
                }

                _context.Set<Employee>().Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }


        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);
        }
    }
}

