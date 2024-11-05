using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Toolkit.Mvvm.DependencyInjection.Abstractions;

namespace Toolkit.Mvvm.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommandFactory(this IServiceCollection services) => 
            services.AddSingleton(typeof(ICommandFactory<>), typeof(CommandFactory<>));

        public static IServiceCollection AddCommandFactory(this IServiceCollection services, Func<object, bool> additionalCondition)
        {
            services
                .AddCommandFactory()
                .AddOptions();

            services
                .TryAddEnumerable(ServiceDescriptor.Singleton((IConfigureOptions<CommandFactoryOptions>)new CommandFactoryAddAdditionalConditionOptions(additionalCondition)));
            return services;
        }
    }
}
