using System;
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

namespace MonitorApplication.Controllers
{
    [Route("api/[controller]")]
   
    public class UsersController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public UsersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDisaptcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
        }

        [HttpPost]
        [ServiceFilter(typeof(FilterWithDI))]
        public IActionResult Post([FromBody] RegisterUserCommand command)
        {
            _commandDispatcher.Execute(command);
            return Ok("");
        }
    }
}