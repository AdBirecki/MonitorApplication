namespace MonitorApplication_BL.Commands.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
