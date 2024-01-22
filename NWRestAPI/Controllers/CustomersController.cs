using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestAPI.Models;

namespace NWRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Alustetaan tietokantayhteys
        NorthwindContext db = new NorthwindContext();

        [HttpGet]

        //Hakee kaikki asiakkaat
        public ActionResult GetAllCustomers()
        {
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }
    }
}
