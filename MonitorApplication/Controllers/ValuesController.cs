using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication_Models;
using MonitorApplicationHttpClient;
using Newtonsoft.Json;


namespace MonitorApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiBaseController
    {
        private readonly GoldClient _goldClient;
        public ValuesController(
            IConfiguration configuration, 
            GoldClient goldClient)
        {
            _goldClient = goldClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            GoldDataDto goldData3 = await _goldClient.GetGoldValues<GoldDataDto>();
            return Ok(goldData3);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
