using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Application.Projects;

[Service]
public class GetProject
{
    private readonly IProjectManager _projectManager;

    public GetProject(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }

    public class ProjectEmployee
    {
        public IEnumerable<Employee> Employees;
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
    }

    public ProjectResponse Do(int id) =>
        _projectManager.GetProjectById(id, x => new ProjectResponse
        {
            Id = x.Id,
            Name = x.Name,
            CustomerCompanyName = x.CustomerCompanyName,
            ExecutorCompanyName = x.ExecutorCompanyName,
            StartDate = x.ProjectStartDate,
            EndDate = x.ProjectEndDate,
            Priority = x.Priority,
            Employees = x.ProjectEmployee.Select(n => new Employee
            {
                Id = n.Employee.Id,
                FirstName = n.Employee.FirstName,
                LastName = n.Employee.LastName,
                MiddleName = n.Employee.MiddleName,
                Email = n.Employee.Email,
                IsManager = n.Employee.IsManager
            })
        });
}