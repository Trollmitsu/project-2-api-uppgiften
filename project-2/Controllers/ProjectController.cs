using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_2.Data;
using project_2.DTO.CustomerDTO;
using project_2.DTO.ProjectDTO;

namespace project_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(_context.Projects.Select(e => new GetProjectsDTO
            {

                ProjectName = e.ProjectName,
                Id = e.Id,
                Customer = new GetCustomerDTO
                {
                    Id = e.Id,
                    Name = e.Customer.Name
                }
                    
            }).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var project = _context.Projects
                .Include(e=>e.Customer)
                .FirstOrDefault(e => e.Id == id);
            if (project == null)
                return NotFound();
            var ret = new GetProjectDTO()
            {
                ProjectName = project.ProjectName,
                Id = project.Id,
                Customer = new GetCustomerDTO()
                {
                    Name = project.Customer.Name,
                    Id = project.Customer.Id
                }

            };
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(CreateProjectDTO CreateNew)
        {
            var customer = _context.Customers.Find(CreateNew.CustomerId);
            var project = new Project
            {
                ProjectName = CreateNew.ProjectName,
                Customer = customer
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            var projectDto = new GetProjectsDTO()
            {
                Id = project.Id,
                ProjectName = project.ProjectName,  
                Customer = new GetCustomerDTO()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                }
            };

            return CreatedAtAction(nameof(GetOne), new { Id = project.Id }, projectDto);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, UpdateProjectDTO UpdateProject)
        {
                var customer = _context.Customers.Find(UpdateProject.CustomerId);
            if (customer == null)
                return NotFound();

            var project = _context.Projects.FirstOrDefault(e => e.Id == id);
            if (project == null)
                return NotFound();

            project.ProjectName = UpdateProject.ProjectName;
            project.Customer = customer;
            customer.Id = UpdateProject.CustomerId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(e => e.Id == id);
            if (project == null) return NotFound();
            _context.Remove(project);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
