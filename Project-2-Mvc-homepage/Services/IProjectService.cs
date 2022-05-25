using System.Text;
using project_2.DTO.ProjectDTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Project_2_Mvc_homepage.Services
{
    public interface IProjectService
    {
        public List<GetProjectsDTO> GetProjects();
        public status CreateProject(CreateProjectDTO CreateNew);
        public status UpdateProject(int id, UpdateProjectDTO updateProject);
        public status DeleteProject(int id);


    }

    public class ProjectService : IProjectService
    {
        private readonly IOptions<ProjectSettings> _settings;

        public ProjectService(IOptions<ProjectSettings> settings)
        {
            _settings = settings;
        }

     

        public List<GetProjectsDTO> GetProjects()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(
                _settings.Value.Url).Result;
            return JsonConvert.DeserializeObject<List<GetProjectsDTO>>(data);
        }

        public status CreateProject(CreateProjectDTO CreateNew)
        {
            var payload = JsonConvert.SerializeObject(CreateNew);
            var httpContent = new StringContent(payload,Encoding.UTF8,"Application/json");
            var httpclient = new HttpClient();
            var data = httpclient.PostAsync(_settings.Value.Url, httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;
        }

        public status UpdateProject(int id, UpdateProjectDTO updateProject)
        {
            var payload = JsonConvert.SerializeObject(updateProject);
            var httpContent = new StringContent(payload, Encoding.UTF8, "Application/json");
            var httpclient = new HttpClient();
            var data = httpclient.PutAsync($"{_settings.Value.Url}/{id}", httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;
        }

        public status DeleteProject(int id)
        {
            var httpclient = new HttpClient();
            var data = httpclient.DeleteAsync($"{_settings.Value.Url}/{id}").Result;
            if (data.IsSuccessStatusCode)
            {
                return status.ok;
            }

            return status.Error;
        }
    }

   
    public class ProjectSettings
    {
        public string Url { get; set; }
    }
}
