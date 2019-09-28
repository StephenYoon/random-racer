using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetricsApi.DataService;
using MetricsApi.Models;

namespace MetricsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IUserService _userService;

        public MetricsController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/metrics
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var results = _userService.GetAll();
            return results.ToList();
        }

        // GET api/metrics/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var results = _userService.GetById(id);
            return results;
        }

        // POST api/metrics
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/metrics/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/metrics/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
