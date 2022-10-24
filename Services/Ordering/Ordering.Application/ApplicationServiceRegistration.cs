using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            //las injecciones van aqui

            //configuramos automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //configuramos fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //configuramos mediatr
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}


/* tipos de injeccion de dependencias
 * 
 * Scooped
 * nos genera una instancia de repo
 * cuando ya no se usa de elimina
 * 
 * 
 * Transient
 * persiste hasta que se acaba la sesion
 * 
 * 
 * 
 * Singleton
 * tal cual singleton una vez iniciado no se elimina
 * 
 */