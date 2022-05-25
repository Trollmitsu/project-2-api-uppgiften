using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using project_2.DTO.CustomerDTO;


namespace Project_2_Mvc_homepage.Services
{
    public interface ICustomerService
    {
        public List<GetCustomersDTO> GetCustomers();

        public status CreateCustomer(CreateCustomerDTO CreateNew);

        public status UpdateCustomer(int id, UpdateCustomerDTO updateCustomer);

        public status DeleteCustomer(int id);
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


        public status CreateCustomer(CreateCustomerDTO CreateNew)
        {
            var payload = JsonConvert.SerializeObject(CreateNew);
            var httpContent = new StringContent(payload, Encoding.UTF8,"Application/json");
            var httpclient = new HttpClient();
            var data = httpclient.PostAsync(_settings.Value.Url, httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;
        }

        public status UpdateCustomer(int id, UpdateCustomerDTO updateCustomer)
        {
            var payload = JsonConvert.SerializeObject(updateCustomer);
            var httpContent = new StringContent(payload, Encoding.UTF8, "Application/json");
            var httpclient = new HttpClient();
            var data = httpclient.PutAsync($"{_settings.Value.Url}/{id}", httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;
        }

        public status DeleteCustomer(int id)
        {
            var httpclient = new HttpClient();
            var data = httpclient.DeleteAsync($"{_settings.Value.Url}/{id}").Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;

        }
    }

    public enum status 
    {
        ok,
        Error
    }

    public class CustomerSettings
    {
        public string Url { get; set; }
    }
}
