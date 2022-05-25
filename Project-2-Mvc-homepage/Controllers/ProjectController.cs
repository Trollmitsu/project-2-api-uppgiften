using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_2.DTO.CustomerDTO;
using project_2.DTO.ProjectDTO;
using Project_2_Mvc_homepage.Services;

using Project_2_Mvc_homepage.ViewModel.ProjectViewModels;


namespace Project_2_Mvc_homepage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICustomerService _customerService;

        public ProjectController(IProjectService projectService, ICustomerService customerService)
        {
            _projectService = projectService;
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            var model = new ProjectViewModel();

            model.Projects = _projectService.GetProjects().Select(e =>
                new ProjectViewModel.ProjectListItemViewModel()
                {
                    ProjectName = e.ProjectName,
                    Id = e.Id,
                    Customer = new GetCustomerDTO()
                    {
                        Name = e.Customer.Name
                    }
                }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var proj = _projectService.GetProjects().FirstOrDefault(e => e.Id == id);
            var model = new EditProjectViewModel
            {
                ProjectName = proj.ProjectName,
                CustomerId = proj.Customer.Id,
                customers = ListItems()

            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, UpdateProjectDTO project)
        {
            if (ModelState.IsValid)
            {
                var proj = _projectService.GetProjects().First(e => e.Id == id);
                var result = _projectService.UpdateProject(id, project);
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Edit));
        }

        public IActionResult Create()
        {
            return View(new CreateProjectViewModel
            {
                customers = ListItems()
            }); 
            
        }

        [HttpPost]
        public IActionResult Create(CreateProjectDTO proj)
        {
            if (ModelState.IsValid)
            {
                _projectService.CreateProject(proj);
                return RedirectToAction(nameof(Index));
            }

            return View(proj);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _projectService.DeleteProject(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public List<SelectListItem> ListItems()
        {
            var list = new List<SelectListItem>();
            var c = _customerService.GetCustomers();
            list.Add(new SelectListItem
            {
                Text = "Select Customer",
                Value = 0.ToString(),
            });
            foreach (var customer in c)
            {
                list.Add(new SelectListItem
                {
                    Text = customer.Name,
                    Value = customer.Id.ToString()
                });
                
            }
            return list;
        }
    }
}
