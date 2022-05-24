using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using project_2.DTO.CustomerDTO;

namespace Project_2_Mvc_homepage.Services
{
    public interface ICustomerService
    {
        List<GetCustomersDTO> GetCustomers();
    }

    public class CustomerService : ICustomerService
    {
        private readonly IOptions<CustomerSettings> _settings;

        public CustomerService(IOptions<CustomerSettings> settings)
        {
            _settings = settings;
        }
        public List<GetCustomersDTO> GetCustomers()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(
                _settings.Value.Url).Result;
            return JsonConvert.DeserializeObject<List<GetCustomersDTO>>(data);
        }
    }

    public class CustomerSettings
    {
        public string Url { get; set; }
    }
}
