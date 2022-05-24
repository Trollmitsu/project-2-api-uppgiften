using project_2.DTO.ProjectDTO;

namespace Project_2_Mvc_homepage.ViewModel.ProjectViewModels
{
    public class ProjectViewModel
    {
        public List<ProjectListItemViewModel> Projects { get; set; }

        public class ProjectListItemViewModel
        {
            public int Id { get; set; }
            public string ProjectName { get; set; }
            
        }
    }
   
}
