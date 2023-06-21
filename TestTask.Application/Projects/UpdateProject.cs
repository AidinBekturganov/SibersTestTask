using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Application.Projects;
[Service]
public class UpdateProject
{
    private IProjectManager _projectManager;

    public UpdateProject(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public async Task<UpdateProjectResponse> Do(UpdateProjectRequest updateProjectRequest)
    {
        var project = _projectManager.GetProjectById(updateProjectRequest.Id, x => x);

        project.Name = updateProjectRequest.Name;
        project.CustomerCompanyName = updateProjectRequest.CustomerCompanyName;
        project.ExecutorCompanyName = updateProjectRequest.ExecutorCompanyName;
        project.StartDate = updateProjectRequest.StartDate;
        project.EndDate = updateProjectRequest.EndDate;
        project.Priority = updateProjectRequest.Priority;

        await _projectManager.UpdateProject(project);

        return new UpdateProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            CustomerCompanyName = project.CustomerCompanyName,
            ExecutorCompanyName = project.ExecutorCompanyName,
            EndDate = project.EndDate,
            StartDate = project.StartDate,
            Priority = project.Priority
        };
    }

    public class UpdateProjectRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public IList<UpdateEmployeeRequest> UpdateEmployeeRequest { get; set; }
    }

    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }
    }

    public class UpdateProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public IEnumerable<UpdateEmployeeResponse> UpdateEmployeeResponse { get; set; }
    }
    
    public class UpdateEmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
    }
}