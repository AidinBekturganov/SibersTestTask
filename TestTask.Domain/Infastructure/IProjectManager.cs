using TestTask.Domain.Models;

namespace TestTask.Domain.Infastructure;

public interface IProjectManager
{
    Task<int> CreateProject(Project project);
    Task<int> DeleteProject(int id);
    Task<int> UpdateProject(Project project);

    TResult GetProjectById<TResult>(int id, Func<Project, TResult> selector);
    TResult GetProjectByName<TResult>(string name, Func<Project, TResult> selector);
    IEnumerable<TResult> GetProjects<TResult>(Func<Project, TResult> selector);
}