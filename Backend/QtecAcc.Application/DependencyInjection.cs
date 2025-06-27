using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace QtecAcc.Application
{
    public static  class ApplicationDependencyInjection
    {
        public static IServiceCollection MediatorDependency(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
