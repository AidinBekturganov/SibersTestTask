using System.Reflection;
using TestTask.Application;
using TestTask.Database;
using TestTask.Domain.Infastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IProjectManager, ProjectManager>();
            @this.AddTransient<IEmployeeManager, EmployeeManager>();
            
            return @this;
        }
    }
}