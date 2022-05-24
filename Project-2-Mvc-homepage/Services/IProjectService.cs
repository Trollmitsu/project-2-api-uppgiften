using project_2.DTO.ProjectDTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Project_2_Mvc_homepage.Services
{
    public interface IProjectService
    {
        public List<GetProjectsDTO> GetProjects();
        public void CreateProject();
        public void UpdateProject();
        public void DeleteProject();


    }

    public class ProjectService : IProjectService
    {
        private readonly IOptions<ProjectSettings> _settings;

        public ProjectService(IOptions<ProjectSettings> settings)
        {
            _settings = settings;
        }
        public void CreateProject()
        {
            throw new NotImplementedException();
        }

        public void DeleteProject()
        {
            throw new NotImplementedException();
        }

        public List<GetProjectsDTO> GetProjects()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(
                _settings.Value.Url).Result;
            return JsonConvert.DeserializeObject<List<GetProjectsDTO>>(data);
        }

        public void UpdateProject()
        {
            throw new NotImplementedException();
        }
    }

    public class ProjectSettings
    {
        public string Url { get; set; }
    }
}
