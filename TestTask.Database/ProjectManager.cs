using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Database;

public class ProjectManager : IProjectManager
{
    private ApplicationDbContext _ctx;

    public ProjectManager(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public Task<int> CreateProject(Project project)
    {
        _ctx.Projects.Add(project);

        return _ctx.SaveChangesAsync();
    }

    public Task<int> DeleteProject(int id)
    {
        var project = _ctx.Projects.FirstOrDefault(x => x.Id == id);
        _ctx.Projects.Remove(project);

        return _ctx.SaveChangesAsync();
    }

    public Task<int> UpdateProject(Project project)
    {
        _ctx.Projects.Update(project);

        return _ctx.SaveChangesAsync();
    }
    

    public TResult GetProjectById<TResult>(int id, Func<Project, TResult> selector)
    {
        return _ctx.Projects
            .Include(x => x.ProjectEmployee)
            .ThenInclude(x => x.Employee)
            .Where(x => x.Id == id)
            .Select(selector)
            .FirstOrDefault();
    }

    public TResult GetProjectByName<TResult>(string name, Func<Project, TResult> selector)
    {
        return _ctx.Projects
            .Include(x => x.ProjectEmployee)
            .ThenInclude(x => x.Employee)
            .Where(x => x.Name == name)
            .Select(selector)
            .FirstOrDefault();
    }

    public IEnumerable<TResult> GetProjects<TResult>(string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        DateTime? startAtBeginDate,
        DateTime? endAtBeginDate,
        DateTime? startAtEndDate,
        DateTime? endAtEndDate,
        Func<Project, TResult> selector)
    {
        IQueryable<Project> projectQuery = _ctx.Projects;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            projectQuery = projectQuery.Where(p => p.Name.Contains(searchTerm)
                                                || p.CustomerCompanyName.Contains(searchTerm)
                                                || p.ExecutorCompanyName.Contains(searchTerm));
        }

        var keySelector = GetSortingProperty<TResult>(sortColumn);

        if (sortOrder?.ToLower() == "desc")
        {
            projectQuery = projectQuery.OrderByDescending(keySelector);
        }
        else
        {
            projectQuery = projectQuery.OrderBy(keySelector);
        }
        
        if (startAtBeginDate.HasValue)
        {
            startAtBeginDate = startAtBeginDate.Value.ToUniversalTime();
            projectQuery = projectQuery.Where(c => startAtBeginDate <= c.ProjectStartDate).AsNoTracking();
        }

        if (endAtBeginDate.HasValue)
        {
            endAtBeginDate = endAtBeginDate.Value.ToUniversalTime();
            projectQuery = projectQuery.Where(c => endAtBeginDate >= c.ProjectStartDate).AsNoTracking();
        }
        
        if (startAtEndDate.HasValue)
        {
            startAtEndDate = startAtEndDate.Value.ToUniversalTime();
            projectQuery = projectQuery.Where(c => startAtEndDate <= c.ProjectEndDate).AsNoTracking();
        }

        if (endAtEndDate.HasValue)
        {
            endAtEndDate = endAtEndDate.Value.ToUniversalTime();
            projectQuery = projectQuery.Where(c => endAtEndDate >= c.ProjectEndDate).AsNoTracking();
        }
        

        return projectQuery
            .Include(x => x.ProjectEmployee)
            .ThenInclude(x => x.Employee)
            .Select(selector)
            .ToList();
    }

    private static Expression<Func<Project, object>> GetSortingProperty<TResult>(string? sortColumn)
    {
        Expression<Func<Project, object>> keySelector = sortColumn?.ToLower() switch
        {
            "name" => project => project.Name,
            "customer_company_name" => project => project.CustomerCompanyName,
            "executor_company_name" => project => project.ExecutorCompanyName,
            "priority" => project => project.Priority,
            _ => project => project.Id
        };
        return keySelector;
    }
}