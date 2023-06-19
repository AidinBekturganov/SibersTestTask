namespace TestTask.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerCompanyName { get; set; }
    public string ExecutorCompanyName { get; set; }
    public int StartDate { get; set; }
    public int EndDate { get; set; }
    public int Priority { get; set; }
}