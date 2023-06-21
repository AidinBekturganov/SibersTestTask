namespace TestTask.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerCompanyName { get; set; }
    public string ExecutorCompanyName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Priority { get; set; }
    
    public ICollection<ProjectEmployee> ProjectEmployee { get; set; }
}