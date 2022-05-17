using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace project_2.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;

        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _context.Database.Migrate();
            SeedCustomers();
            SeedProjects();
            SeedTimeRegistration();
        }

        private void SeedCustomers()
        {
            if (!_context.Customers.Any(e => e.Name == "Stefan Holmberg"))
            {
                var cust = _context.Customers.Add(new Customer
                {
                    Name = "Stefan Holmberg",
                    
                });
            }
            if (!_context.Customers.Any(e => e.Name == "Danish Gill"))
            {
                var cust = _context.Customers.Add(new Customer
                {
                    Name = "Danish Gill"
                });
            }
            if (!_context.Customers.Any(e => e.Name == "Per Görtz"))
            {
                var cust = _context.Customers.Add(new Customer
                {
                    Name = "Per Görtz"
                });
            }
            _context.SaveChanges();
        }

        private void SeedProjects()
        {
            if (!_context.Projects.Any(e => e.ProjectName == "Project X"))
            {
                var cust = _context.Customers.First(e => e.Name == "Stefan Holmberg");
                var proj = _context.Projects.Add(new Project()
                {
                    ProjectName = "Project X",
                    Customer = cust

                });
            }
            if (!_context.Projects.Any(e => e.ProjectName == "Scania"))
            {
                var cust = _context.Customers.First(e => e.Name == "Danish Gill");
                var proj = _context.Projects.Add(new Project()
                {
                    ProjectName = "Scania",
                    Customer = cust

                });
            }
            if (!_context.Projects.Any(e => e.ProjectName == "Dustin"))
            {
                var cust = _context.Customers.First(e => e.Name == "Per Görtz");
                var proj = _context.Projects.Add(new Project()
                {
                    ProjectName = "Dustin",
                    Customer = cust
                });
            }

            _context.SaveChanges();
        }

        private void SeedTimeRegistration()
        {
            if (!_context.TimeRegistration
                    .Include(e => e.Customer)
                    .Include(e => e.Project)
                    .Any(e => e.Description == "project 1"))

            {
                var cust = _context.Customers.First(e => e.Name == "Stefan Holmberg");
                var proj = _context.Projects.First(e => e.ProjectName == "Project X");
                _context.TimeRegistration.Add(new TimeRegistration()
                {
                    Date = DateTime.Now,
                    AmountTime = 200,
                    Description = "project 1",
                    Customer = cust,
                    Project = proj
                });
            }
            if (!_context.TimeRegistration
                    .Include(e => e.Customer)
                    .Include(e => e.Project)
                    .Any(e => e.Description == "project 2"))
            { var cust = _context.Customers.First(e => e.Name == "Danish Gill");
                var proj = _context.Projects.First(e => e.ProjectName == "scania");
                _context.TimeRegistration.Add(new TimeRegistration()
                {
                    Date = DateTime.Now,
                    AmountTime = 500,
                    Description = "project 2",
                    Customer = cust,
                    Project = proj
                    
                });
            }

            if (!_context.TimeRegistration
                    .Include(e => e.Customer)
                    .Include(e => e.Project)
                    .Any(e => e.Description == "project 3"))

            {
                var cust = _context.Customers.First(e => e.Name == "Per Görtz");
                var proj = _context.Projects.First(e => e.ProjectName == "Dustin");
                _context.TimeRegistration.Add(new TimeRegistration()
                {
                    Date = DateTime.Now,
                    AmountTime = 100,
                    Description = "project 3",
                    Customer = cust,
                    Project = proj
                });
            }

            _context.SaveChanges();
        }
    }
}
