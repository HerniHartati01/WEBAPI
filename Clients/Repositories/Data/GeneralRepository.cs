using Clients.Repositories.Interface;
using Clients.ViewModels.Others;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Clients.Repositories.Data
{
    public class GeneralRepository<TEntity, T> : IGeneralRepository<TEntity, T>
        where TEntity : class
    {
        private readonly string request;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor contextAccessor;

        public GeneralRepository(string request)
        {
            this.request = request;
           /* contextAccessor = new HttpContextAccessor();*/
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7148/api/")
            };
            // Ini yg bawah skip dulu
            /*httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));*/
        }
        public async Task<ResponseMessageVM> Delete(T id)
        {
            ResponseMessageVM? entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseVM<TEntity>> Get(T id)
        {
            ResponseVM<TEntity>? entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseVM<TEntity>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseListVM<TEntity>> Get()
        {
            ResponseListVM<TEntity>? entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<TEntity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Post(TEntity entity)
        {
            ResponseMessageVM? entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Put(TEntity entity)
        {
            ResponseMessageVM? entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }
    }
}
