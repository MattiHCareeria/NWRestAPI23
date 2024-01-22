using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NWRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return ("Hello World");
        }
    }
}
