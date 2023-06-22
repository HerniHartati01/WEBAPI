using Clients.Models;
using Clients.Repositories.Interface;

namespace Clients.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public EmployeeRepository(string request = "Employee/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7148/api/")
            };
            this.request = request;

        }
    }
}
