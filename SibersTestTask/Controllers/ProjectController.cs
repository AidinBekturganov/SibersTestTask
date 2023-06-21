using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Projects;

namespace SibersTestTask.Controllers;

[Route("project")]
public class ProjectController : Controller
{
    [HttpPost("")]
    public async Task<IActionResult> CreateProject( [FromBody] CreateProject.ProjectRequest request,
        [FromServices] CreateProject createProject) =>
        Ok((await createProject.Do(request)));
    
    [HttpGet("{id}")]
    public IActionResult GetProject(
        int id,
        [FromServices] GetProject getProject) =>
        Ok(getProject.Do(id));
    
    [HttpGet("")]
    public IActionResult GetProjects([FromServices] GetProjects getProjects) =>
        Ok(getProjects.Do());
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(
        int id,
        [FromServices] DeleteProject deleteProject) =>
        Ok((await deleteProject.Do(id)));
    
    [HttpPut("")]
    public async Task<IActionResult> UpdateProject(
        [FromBody] UpdateProject.UpdateProjectRequest request,
        [FromServices] UpdateProject updateProject) =>
        Ok((await updateProject.Do(request)));

    // [HttpGet("{id}")]
    // public IActionResult GetOrder(
    //     int id,
    //     [FromServices] GetOrder getOrder) =>
    //     Ok(getOrder.Do(id));
    //
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateOrder(
    //     int id,
    //     [FromServices] UpdateOrder updateOrder)
    // {
    //     var success = await updateOrder.DoAsync(id) > 0;
    //     if (success)
    //     {
    //         return Ok();
    //     }
    //     else
    //     {
    //         return BadRequest();
    //     }
    // }
}