using Microsoft.AspNetCore.Mvc;
using Project_2_Mvc_homepage.Services;
using Project_2_Mvc_homepage.ViewModel;
using Project_2_Mvc_homepage.ViewModel.CustomerViewModels;

namespace Project_2_Mvc_homepage.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            var model = new CustomersViewModel();
            model.Customers = _customerService.GetCustomers().Select(e =>
                new CustomersViewModel.CustomerListItemViewModel
                {
                    Name = e.Name,
                    Id = e.Id
                }).ToList();
            
            return View(model);
        }
    }
}
