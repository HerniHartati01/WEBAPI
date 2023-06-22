using Clients.Models;
using Clients.Repositories.Interface;

namespace Clients.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, Guid>, IUniversityRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public UniversityRepository(string request = "University/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7148/api/")
            };
            this.request = request;

        }

    }
}
