using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Employees;

namespace Microsoft.Extensions.DependencyInjection.Controllers;

[Route("[controller]")]
public class EmployeeController : Controller
{
    [HttpPost("")]
    public async Task<IActionResult> CreateEmployee( [FromBody] CreateEmployee.EmployeeRequest request,
        [FromServices] CreateEmployee createEmployee) =>
        Ok((await createEmployee.Do(request)));
}