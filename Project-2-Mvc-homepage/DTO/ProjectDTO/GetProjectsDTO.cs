
using project_2.DTO.CustomerDTO;

namespace project_2.DTO.ProjectDTO
{
    public class GetProjectsDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public GetCustomerDTO Customer { get; set; }
    }
}
