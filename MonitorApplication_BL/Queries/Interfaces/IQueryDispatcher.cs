namespace MonitorApplication_BL.Queries.Interfaces
{
    public interface IQueryDispatcher
    {
        void Execute<TQuery>(TQuery tQuery) where TQuery : IQuery;
    }
}
