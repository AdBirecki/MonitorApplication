namespace MonitorApplication_BL.Queries.Interfaces
{
    public interface IQueryHandler<TQuery> where TQuery : IQuery
    {
        void Execute(TQuery tQuery);
    }
}
