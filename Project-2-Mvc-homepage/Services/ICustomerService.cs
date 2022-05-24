using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using project_2.DTO.CustomerDTO;


namespace Project_2_Mvc_homepage.Services
{
    public interface ICustomerService
    {
        public List<GetCustomersDTO> GetCustomers();

        public void CreateCustomer();

        public void UpdateCustomer();

        public void DeleteCustomer();
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

        public void CreateCustomer()
        {
            //var httpClient = new HttpClient();
            //var data = httpClient.PostAsync(
            //    _settings.Value.Url).Result;
            //JsonConvert.DeserializeObject<List<CreateCustomerDTO>>(data);
        }

        public void UpdateCustomer()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(
                _settings.Value.Url).Result;
            JsonConvert.DeserializeObject<List<UpdateCustomerDTO>>(data);
        }

        public void DeleteCustomer()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(
                _settings.Value.Url).Result;
            JsonConvert.DeserializeObject<List<DeleteCustomerDTO>>(data);

        }
    }

    public class CustomerSettings
    {
        public string Url { get; set; }
    }
}
