using System.ComponentModel.DataAnnotations;

namespace Project_2_Mvc_homepage.ViewModel.ProjectViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; }
    }
}
