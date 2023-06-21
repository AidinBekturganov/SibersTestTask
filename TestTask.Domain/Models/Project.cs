namespace TestTask.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerCompanyName { get; set; }
    public string ExecutorCompanyName { get; set; }
    public DateTime? ProjectStartDate { get; set; }
    public DateTime? ProjectEndDate { get; set; }
    public int Priority { get; set; }
    
    public ICollection<ProjectEmployee> ProjectEmployee { get; set; }
}