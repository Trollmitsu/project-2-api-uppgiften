using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_2.Data;
using project_2.DTO.CustomerDTO;
using project_2.DTO.ProjectDTO;
using project_2.DTO.TimeRegDTO;

namespace project_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeRegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_context.TimeRegistration.Select(e=> new GetTimeRegsDTO
            {
                CustomerId = e.Customer.Id,
                ProjectId = e.Project.Id,
                AmountTime = e.AmountTime,
                Date = e.Date,
                Description = e.Description,
                Id = e.Id

            }).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var timereg = _context.TimeRegistration
                .Include(e => e.Customer)
                .Include(e=>e.Project)
                .FirstOrDefault(e => e.Id == id);
            if (timereg == null)
                return NotFound();
            
            var ret = new GetTimeRegDTO()
            {
                AmountTime = timereg.AmountTime,
                Id = timereg.Id,
                Description = timereg.Description,


                Customer = timereg.Customer.Id,
                Project = timereg.Project.Id

            };
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(CreateTimeRegDTO CreateNew)
        {
            var customer = _context.Customers.Find(CreateNew.CustomerId);
            var project = _context.Projects.Find(CreateNew.ProjectId);
            var timereg = new TimeRegistration
            {
                Date = CreateNew.Date,
                AmountTime = CreateNew.AmountTime,
                Description = CreateNew.Description,
                Project = project,
                Customer = customer
            };
            _context.TimeRegistration.Add(timereg);
            _context.SaveChanges();
            var timeregDTO = new GetTimeRegsDTO()
            {
                Id = timereg.Id,
                Date = timereg.Date,
                Description = timereg.Description,
                AmountTime = timereg.AmountTime,
                CustomerId = timereg.Customer.Id,
                ProjectId = timereg.Project.Id
                
            };

            return CreatedAtAction(nameof(GetOne), new { Id = timeregDTO.Id }, timeregDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, UpdateTimeRegDTO UpdateTimeReg)
        {
            //var customer = _context.Customers.Find(UpdateTimeReg.CustomerId);
            //if (customer == null)
            //    return NotFound();

            //var project = _context.Projects.Find(UpdateTimeReg.ProjectId);
            //if (project == null)
            //    return NotFound();

            var timereg = _context.TimeRegistration.FirstOrDefault(e => e.Id == id);
            if (timereg == null)
                return NotFound();


            timereg.AmountTime = UpdateTimeReg.AmountTime;
            timereg.Date = UpdateTimeReg.Date;
            timereg.Description = UpdateTimeReg.Description;


            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var timereg = _context.TimeRegistration.FirstOrDefault(e => e.Id == id);
            if (timereg == null)
                return NotFound();
            _context.Remove(timereg);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
