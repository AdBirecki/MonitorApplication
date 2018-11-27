using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication_BL.Queries.RetriveUserQueries
{
    /* Retrive user by name */
    public class RetriveUserQuery : IQuery<User>
    {
        public string UserName { get; set; }

    }
}
