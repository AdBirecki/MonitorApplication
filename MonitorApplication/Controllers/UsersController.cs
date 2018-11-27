using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication.Filters;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.RetriveUserQueries;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class UsersController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public UsersController(
            ICommandDispatcher commandDispatcher, 
            IQueryDispatcher queryDisaptcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
        }

        [HttpPost]
        [ServiceFilter(typeof(FilterWithDI))]
        public IActionResult PostUser([FromBody] RegisterUserCommand command)
        {
            _commandDispatcher.Execute(command);
            return Ok("");
        }

        [HttpPost]
        public IActionResult GetUser([FromBody] RetriveUserQuery query)
        {
            User user = _queryDispatcher.Execute<RetriveUserQuery,User>(query);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult GetAllUsers([FromBody] RetriveUsersQuery query)
        {
            IEnumerable allUsers = _queryDispatcher.Execute<RetriveUsersQuery, IEnumerable<User>>(query);
            return Ok(allUsers);
        }
    }
}