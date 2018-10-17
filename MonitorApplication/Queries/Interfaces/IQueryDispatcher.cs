using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Queries.Interfaces
{
    interface IQueryDispatcher
    {
        void Execute<TQuery>(TQuery tQuery) where TQuery : IQuery;
    }
}
