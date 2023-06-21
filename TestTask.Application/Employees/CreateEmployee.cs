using TestTask.Application.Projects;
using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Application.Employees;

[Service]
public class CreateEmployee
{
    private IEmployeeManager _employeeManager;

    public CreateEmployee(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }
    
    public class EmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
    }
    
    public async Task<bool> Do(EmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            IsManager = request.IsManager,
            ProjectEmployee = new List<ProjectEmployee>()
        };

        var success = await _employeeManager.CreateEmployee(employee) > 0;

        if (success)
        {
            return true;
        }
        
        return false;
    }
}