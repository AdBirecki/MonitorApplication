using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Queries.RetriveUserQueries
{
    public class RetriveAllUsersQuery : IQuery<IEnumerable<User>>
    {
    }
}
