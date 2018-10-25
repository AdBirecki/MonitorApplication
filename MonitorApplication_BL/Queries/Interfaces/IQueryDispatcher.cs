namespace MonitorApplication_BL.Queries.Interfaces
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery,TResult>(TQuery tQuery) where TQuery : IQuery<TResult>;
    }
}
