using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Infastructure;
using TestTask.Domain.Models;

namespace TestTask.Database;

public class EmployeeManager : IEmployeeManager
{
    private ApplicationDbContext _ctx;

    public EmployeeManager(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public Task<int> CreateEmployee(Employee employee)
    {
        _ctx.Employees.Add(employee);

        return _ctx.SaveChangesAsync();
    }

    public Task<int> DeleteEmployee(int id)
    {
        var employee = _ctx.Employees.FirstOrDefault(x => x.Id == id);
        _ctx.Employees.Remove(employee);

        return _ctx.SaveChangesAsync();
    }

    public Task<int> UpdateEmployee(Employee employee)
    {
        _ctx.Employees.Update(employee);

        return _ctx.SaveChangesAsync();
    }

    public TResult GetEmployeeById<TResult>(int id, Func<Employee, TResult> selector)
    {
        return _ctx.Employees
            .Include(x => x.ProjectEmployee)
            .ThenInclude(x => x.Project)
            .Where(x => x.Id == id)
            .Select(selector)
            .FirstOrDefault();
    }
    

    public IEnumerable<TResult> GetEmployeesWithProject<TResult>(Func<Employee, TResult> selector)
    {
        throw new NotImplementedException();
    }
}