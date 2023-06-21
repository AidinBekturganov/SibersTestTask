using TestTask.Domain.Models;

namespace TestTask.Domain.Infastructure;

public interface IEmployeeManager
{
    Task<int> CreateEmployee(Employee employee);
    Task<int> DeleteEmployee(int id);
    Task<int> UpdateEmployee(Employee employee);

    TResult GetEmployeeById<TResult>(int id, Func<Employee, TResult> selector);
    IEnumerable<TResult> GetEmployeesWithProject<TResult>(Func<Employee, TResult> selector);
}