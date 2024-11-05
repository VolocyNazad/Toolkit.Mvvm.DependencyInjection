using Microsoft.Extensions.Options;

namespace Toolkit.Mvvm.DependencyInjection
{
    internal class CommandFactoryAddAdditionalConditionOptions : ConfigureOptions<CommandFactoryOptions>
    {
        public CommandFactoryAddAdditionalConditionOptions(Func<object, bool> additionalCondition)
            : base(delegate (CommandFactoryOptions options)
            {
                options.AdditionalCondition = additionalCondition;
            })
        {
        }
    }
}