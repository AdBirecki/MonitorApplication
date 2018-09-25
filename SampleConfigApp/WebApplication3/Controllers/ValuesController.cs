using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValuesInterface _valuesService;

        public ValuesController(IValuesInterface ivaluesInterface)
        {
            _valuesService = ivaluesInterface;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var value = _valuesService.GetValue();
            return Ok(new string[] { "value1", "value2", value.ToString() });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
