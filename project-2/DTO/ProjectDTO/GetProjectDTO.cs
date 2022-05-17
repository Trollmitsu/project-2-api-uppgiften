using project_2.Data;
using project_2.DTO.CustomerDTO;

namespace project_2.DTO.ProjectDTO
{
    public class GetProjectDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public GetCustomerDTO Customer { get; set; }
    }
}
