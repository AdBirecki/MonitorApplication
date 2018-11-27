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
    public class MineralsController : ApiBaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public MineralsController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDisaptcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDisaptcher;
        }

        [HttpPost]
        public IActionResult GetPrices([FromBody] RetriveMineralPricesQuery query)
        {
           IEnumerable<MineralPriceData> mpdCollection =  _queryDispatcher
                .Execute<RetriveMineralPricesQuery,IEnumerable<MineralPriceData>> (query);
           return Ok(mpdCollection);
        }

    }
}
