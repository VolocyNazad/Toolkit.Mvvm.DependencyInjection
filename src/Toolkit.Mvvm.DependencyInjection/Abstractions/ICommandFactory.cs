using System.Runtime.CompilerServices;
using Toolkit.Mvvm.Infrastructure.Commands.Base;

namespace Toolkit.Mvvm.DependencyInjection.Abstractions
{
    public interface ICommandFactory<out TViewModel> : ICommandFactory
    {
    }

    public interface ICommandFactory
    {
        Command Create(
            Action<object> execute, Func<object, bool> canExecute, [CallerMemberName] string memberName = null!);

        AsyncCommand CreateAsync(
            Func<object, Task> execute, Func<object, bool> canExecute, [CallerMemberName] string memberName = null!);

        Command Create(
            Action<object> execute, [CallerMemberName] string memberName = null!);

        AsyncCommand CreateAsync(
            Func<object, Task> execute, [CallerMemberName] string memberName = null!);
    }
}
