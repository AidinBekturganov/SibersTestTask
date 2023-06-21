namespace TestTask.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public bool IsManager { get; set; }
    
    public ICollection<ProjectEmployee> ProjectEmployee { get; set; }
}