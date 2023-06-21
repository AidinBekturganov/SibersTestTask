using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Application.Projects;

[Service]
public class CreateProject
{
    private IProjectManager _projectManager;

    public CreateProject(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public class ProjectRequest
    {
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }

        public List<Employee>? Employees { get; set; }
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
    }
    
    public async Task<bool> Do(ProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name,
            CustomerCompanyName = request.CustomerCompanyName,
            ExecutorCompanyName = request.ExecutorCompanyName,
            ProjectStartDate = request.StartDate,
            ProjectEndDate = request.EndDate,
            Priority = request.Priority,

            ProjectEmployee = request.Employees.Select(x => new ProjectEmployee()
            {
                EmployeeId = x.EmployeeId
            }).ToList()
        };

        var success = await _projectManager.CreateProject(project) > 0;

        if (success)
        {
            return true;
        }
        
        return false;
    }
    
}