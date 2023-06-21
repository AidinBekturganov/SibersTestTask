using TestTask.Domain.Infastructure;

namespace TestTask.Application.Projects;

[Service]
public class GetProjects
{
    private IProjectManager _projectManager;

    public GetProjects(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public IEnumerable<ProjectViewModel> Do(string? searchTerm, string? sortColumn,
        string? sortOrder,
        DateTime? startAtBeginDate,
        DateTime? endAtBeginDate,
        DateTime? startAtEndDate,
        DateTime? endAtEndDate) =>
        _projectManager.GetProjects(searchTerm, sortColumn, sortOrder,
            startAtBeginDate,
            endAtBeginDate,
            startAtEndDate,
            endAtEndDate,
            x => new ProjectViewModel
        {
            Id = x.Id,
            Name = x.Name,
            CustomerCompanyName = x.CustomerCompanyName,
            ExecutorCompanyName = x.ExecutorCompanyName,
            StartDate = x.ProjectStartDate,
            EndDate = x.ProjectEndDate,
            Priority = x.Priority,
            EmployeeVieModel = x.ProjectEmployee.Select(n => new EmployeeVieModel
            {
                Id = n.Employee.Id,
                FirstName = n.Employee.FirstName,
                LastName = n.Employee.LastName,
                MiddleName = n.Employee.MiddleName,
                Email = n.Employee.Email,
                IsManager = n.Employee.IsManager,
            }),
        });

    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompanyName { get; set; }
        public string ExecutorCompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }

        public IEnumerable<EmployeeVieModel> EmployeeVieModel { get; set; }
    }

    public class EmployeeVieModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
    }
}