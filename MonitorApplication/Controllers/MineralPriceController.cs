using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Queries.Interfaces;
using MonitorApplication_BL.Queries.Queries;
using MonitorApplication_Models;
using MonitorApplication_Models.OrderModel;
using MonitorApplicationHttpClient;
using Newtonsoft.Json;


namespace MonitorApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MineralPriceController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public MineralPriceController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDisaptcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetMineralData([FromBody] RetriveMineralPricesQuery query)
        {
           IEnumerable<MineralPriceData> allMPD =  _queryDispatcher.Execute<RetriveMineralPricesQuery,IEnumerable<MineralPriceData>> (query);
           return Ok(allMPD);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
