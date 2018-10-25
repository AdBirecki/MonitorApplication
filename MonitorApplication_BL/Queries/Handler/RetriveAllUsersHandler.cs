using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Queries.Handler
{
    public class RetriveAllUsersHandler : IQueryHandler<RetriveAllUsersQuery, IEnumerable<User>>
    {

        public IEnumerable<User> Execute(RetriveAllUsersQuery tQuery)
        {
            return new List<User> { };
        }
    }
}
