using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_2_Mvc_homepage.ViewModel.ProjectViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<SelectListItem> customers { get; set; }
    }
}
