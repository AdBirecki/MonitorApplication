using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MonitorApplication_BL.Queries.Interfaces;

namespace MonitorApplication_BL.Queries.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _componentContext;
        public QueryDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Execute<TQuery>(TQuery tQuery) where TQuery : IQuery
        {
            IQueryHandler<TQuery> handler = _componentContext.Resolve<IQueryHandler<TQuery>>();
            handler.Execute(tQuery);
        }
    }
}
