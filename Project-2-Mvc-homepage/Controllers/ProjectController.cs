using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
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
            var model = new EditProjectViewModel();
            model.ProjectName = proj.ProjectName;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var proj = _projectService.GetProjects().SingleOrDefault(e => e.Id == id);
                proj.ProjectName = project.ProjectName;


                return RedirectToAction(nameof(Index));

            }
            return View(project);
        }

        public IActionResult Create()
        {
            var proj = new CreateProjectViewModel();
            return View(proj);
        }

        [HttpPost]
        public IActionResult Create(CreateProjectViewModel proj)
        {
            if (ModelState.IsValid)
            {
                var project = new CreateProjectDTO();
                project.ProjectName = proj.ProjectName;
                return RedirectToAction(nameof(Index));
            }

            return View(proj);
        }
    }
}
