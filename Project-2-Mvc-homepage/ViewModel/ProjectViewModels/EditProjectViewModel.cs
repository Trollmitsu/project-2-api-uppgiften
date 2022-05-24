using System.ComponentModel.DataAnnotations;

namespace Project_2_Mvc_homepage.ViewModel.ProjectViewModels
{
    public class EditProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; }
    }
}
