using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetValueController : Controller
    {
        private readonly IRedisConnectionFactory _fact;

        public SetValueController(IRedisConnectionFactory fact)
        {
            _fact = fact;
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
            var db = _fact.GetDatabase();
            db.StringSet("CacheTest", value);
        }
    }
}
