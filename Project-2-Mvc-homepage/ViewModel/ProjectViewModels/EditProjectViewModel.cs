using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_2_Mvc_homepage.ViewModel.ProjectViewModels
{
    public class EditProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; }

        
        public int CustomerId { get; set; }

        
        public List<SelectListItem> customers { get; set; }
    }
}
