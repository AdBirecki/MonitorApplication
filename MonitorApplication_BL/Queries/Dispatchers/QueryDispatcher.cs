using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_BL.Queries.Interfaces;

namespace MonitorApplication_BL.Queries.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        public void Execute<TQuery>(TQuery tQuery) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
