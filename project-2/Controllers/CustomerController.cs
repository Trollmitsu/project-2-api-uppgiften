using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_2.Data;
using project_2.DTO.CustomerDTO;

namespace project_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Customers.Select(e => new GetCustomersDTO
            {
                Name = e.Name,
                Id = e.Id

            }).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var customer = _context.Customers.FirstOrDefault(e => e.Id == id);
            if (customer == null)
                return NotFound();
            var ret = new GetCustomerDTO
            {
                Name = customer.Name,
                Id = customer.Id
            };
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerDTO CreateNew)
        {
            var customer = new Customer()
            {
                Name = CreateNew.Name
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            var custdto = new GetCustomersDTO()
            {
                Id = customer.Id,
                Name = customer.Name
            };

            return CreatedAtAction(nameof(GetOne), new { Id = customer.Id }, custdto);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, UpdateCustomerDTO UpdateCustomer)
        {
            var customer = _context.Customers.FirstOrDefault(e => e.Id == id);
            if (customer == null)
                return NotFound();

            customer.Name = UpdateCustomer.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(e => e.Id == id);
            if (customer == null) return NotFound();
            _context.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }

    
    }   
}
