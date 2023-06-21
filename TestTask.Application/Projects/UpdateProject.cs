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
        project.ProjectStartDate = updateProjectRequest.StartDate;
        project.ProjectEndDate = updateProjectRequest.EndDate;
        project.Priority = updateProjectRequest.Priority;
        project.ProjectEmployee = updateProjectRequest.UpdateEmployeeRequest.Select(x => new ProjectEmployee
        {
            EmployeeId = x.Id,
            ProjectId = updateProjectRequest.Id
        }).ToList();

        await _projectManager.UpdateProject(project);

        var updatedProject = _projectManager.GetProjectById(updateProjectRequest.Id, x => x);
        
        return new UpdateProjectResponse
        {
            Id = updatedProject.Id,
            Name = updatedProject.Name,
            CustomerCompanyName = updatedProject.CustomerCompanyName,
            ExecutorCompanyName = updatedProject.ExecutorCompanyName,
            EndDate = updatedProject.ProjectEndDate,
            StartDate = updatedProject.ProjectStartDate,
            Priority = updatedProject.Priority,
            UpdateEmployeeResponse = updatedProject.ProjectEmployee.Select(n => new UpdateEmployeeResponse
            {
                Email = n.Employee.Email,
                Id = n.Employee.Id,
                FirstName = n.Employee.FirstName,
                IsManager = n.Employee.IsManager,
                LastName = n.Employee.LastName,
                MiddleName = n.Employee.MiddleName
            }).ToList()
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