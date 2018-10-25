namespace MonitorApplication_BL.Queries.Interfaces
{
    public interface IQueryHandler<TQuery,TResponse> where TQuery : IQuery<TResponse>
    {
        TResponse Execute(TQuery tQuery);
    }
}
