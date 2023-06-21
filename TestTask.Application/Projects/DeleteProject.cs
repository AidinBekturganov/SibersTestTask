using TestTask.Domain.Infastructure;

namespace TestTask.Application.Projects;

[Service]
public class DeleteProject
{
    private IProjectManager _projectManager;

    public DeleteProject(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public Task<int> Do(int id)
    {
        return _projectManager.DeleteProject(id);
    }
}