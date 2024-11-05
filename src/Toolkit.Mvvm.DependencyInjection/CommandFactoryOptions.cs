namespace Toolkit.Mvvm.DependencyInjection
{
    public class CommandFactoryOptions
    {
        public Func<object, bool>? AdditionalCondition { get; set; }

    }
}