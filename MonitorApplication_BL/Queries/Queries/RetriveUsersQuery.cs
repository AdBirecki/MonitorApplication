using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_BL.Queries.RetriveUserQueries
{
    /*  This class currently supports no filters. Usually filter values such as equal, older, younger, name starting with, etc. woudl be palced here */
    public class RetriveUsersQuery : IQuery<IEnumerable<User>>
    {
    }
}
