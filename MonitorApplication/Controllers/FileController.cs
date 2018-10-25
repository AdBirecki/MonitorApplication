using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.RegisterCommand;
using MonitorApplication_BL.Queries.Interfaces;

namespace MonitorApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public FileController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDisaptcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
        }

        [HttpPost]
        public IActionResult PostFile([FromBody] SaveFileCommand command)
        {
            _commandDispatcher.Execute(command);
            return Ok("Ok");
        }
    }
}