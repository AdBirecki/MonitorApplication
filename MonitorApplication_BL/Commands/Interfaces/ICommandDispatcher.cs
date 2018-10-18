namespace MonitorApplication_BL.Commands.Interfaces
{
    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand commmand) where TCommand : ICommand;
    }
}
