using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Queries.Interfaces
{
    interface IQueryHandler<TQuery> where TQuery : IQuery
    {
        void Execute(TQuery tQuery);
    }
}
