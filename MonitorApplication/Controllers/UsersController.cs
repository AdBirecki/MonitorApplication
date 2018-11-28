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
        private const string UserNotFound = "User with specified username was not found!";
        private const string NoUsersNotFound = "No users were found!";

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
            try
            {
               _commandDispatcher.Execute(command);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult GetUser([FromBody] RetriveUserQuery query)
        {
            try {
                User user = _queryDispatcher.Execute<RetriveUserQuery, User>(query);
                if (user == null) {
                    return BadRequest(UserNotFound);
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult GetAllUsers([FromBody] RetriveUsersQuery query)
        {
            try {
                IEnumerable allUsers = _queryDispatcher.Execute<RetriveUsersQuery, IEnumerable<User>>(query);
                if (allUsers == null) {
                    return BadRequest(NoUsersNotFound);
                }
                return Ok(allUsers);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
    }
}