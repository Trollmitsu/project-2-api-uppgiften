﻿using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_2_Mvc_homepage.Services;
using Project_2_Mvc_homepage.ViewModel;
using Project_2_Mvc_homepage.ViewModel.CustomerViewModels;
using project_2;
using project_2.DTO.CustomerDTO;

namespace Project_2_Mvc_homepage.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cust = _customerService.GetCustomers().FirstOrDefault(e => e.Id == id);
            var model = new EditCustomerViewModel();
            model.Name = cust.Name;
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Edit([Bind("Name")]int id, UpdateCustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                
                var cust = _customerService.GetCustomers().First(e => e.Id == id);
                var result = _customerService.UpdateCustomer(id, customer);


                return RedirectToAction(nameof(Index));


            }

            return RedirectToAction(nameof(Edit));
        }

        public IActionResult Create()
        {
            var cust = new CreateCustomerViewModel();
            
            return View(cust);
        }

        [HttpPost]
        public IActionResult Create(
            CreateCustomerDTO cust)
        {

            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(cust);
                return RedirectToAction(nameof(Index));
            }

            return View(cust);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _customerService.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
