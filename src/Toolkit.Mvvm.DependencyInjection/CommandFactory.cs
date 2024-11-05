using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using Toolkit.Mvvm.DependencyInjection.Abstractions;
using Toolkit.Mvvm.Infrastructure.Commands;
using Toolkit.Mvvm.Infrastructure.Commands.Base;

namespace Toolkit.Mvvm.DependencyInjection
{
    public sealed class CommandFactory<TViewModel> : ICommandFactory<TViewModel>
    {
        private readonly CommandFactoryOptions _options;
        private readonly ILogger<TViewModel> _logger;

        private Func<object, bool> AdditionalCondition => _options.AdditionalCondition ?? (i => true);

        public Command Create(Action<object> execute, [CallerMemberName] string memberName = null!)
        {
            LambdaCommand command = new LambdaCommand(execute, AdditionalCondition, memberName);
            command.CanExecuteFinished += Command_CanExecuteFinished;
            command.Created += Command_Created;
            command.Executed += Command_Executed;
            return command;
        }

        public AsyncCommand CreateAsync(Func<object, Task> execute, [CallerMemberName] string memberName = null!)
        {
            LambdaAsyncCommand command = new LambdaAsyncCommand(execute, AdditionalCondition, memberName);
            command.ExceptionInvoked += Command_ExceptionInvoked;
            command.CanExecuteFinished += Command_CanExecuteFinished;
            command.Created += Command_Created;
            command.Executed += Command_Executed;
            return command;
        }

        public Command Create(Action<object> execute, Func<object, bool> canExecute, [CallerMemberName] string memberName = null!)
        {
            LambdaCommand command = new LambdaCommand(execute, i => AdditionalCondition(i) && canExecute(i), memberName);
            command.CanExecuteFinished += Command_CanExecuteFinished;
            command.Created += Command_Created;
            command.Executed += Command_Executed;
            return command;
        }

        public AsyncCommand CreateAsync(Func<object, Task> execute, Func<object, bool> canExecute, [CallerMemberName] string memberName = null!)
        {
            LambdaAsyncCommand command = new LambdaAsyncCommand(execute, i => AdditionalCondition(i) && canExecute(i), memberName);
            command.ExceptionInvoked += Command_ExceptionInvoked;
            command.CanExecuteFinished += Command_CanExecuteFinished;
            command.Created += Command_Created;
            command.Executed += Command_Executed;
            return command;
        }

        private void Command_Executed(object s, CommandExecutedEventArgs e)
        {
            _logger?.LogTrace($"Command '{e.Name}' finished");
        }

        private void Command_Created(object s, CommandCreatedArgs e)
        {
            _logger?.LogTrace($"Command '{e.Name}' is created");
        }

        private void Command_ExceptionInvoked(object s, CommandExceptionInvokedEventArgs e)
        {
            _logger?.Log(LogLevel.Error, e.Exception, "An error occurred in an asynchronous command.");
        }

        private void Command_CanExecuteFinished(object s, CommandCanExecuteFinishedArgs e)
        {
            if (!e.Result)
                _logger?.LogTrace($"Command '{e.Name}' cannot be executed");
        }

        #region Constructors

        public CommandFactory(
            IOptions<CommandFactoryOptions> configureOptions,
            ILogger<TViewModel> logger)
        {
            _options = configureOptions.Value;
            _logger = logger;
        } 

        #endregion

    }
}
