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
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }



        }

        [HttpGet("{id}")]

        //Hakee yhen asiakkaan pääavaimella
        public ActionResult GetCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    //return BadRequest("Asiakasta ei löydy idllä " + id + ".");
                    return BadRequest($"Asiakasta ei löydy idllä {id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }


        }

        //Uuden lisääminen

        [HttpPost]

        public ActionResult AddNew([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok($"Added new customer {cust.CompanyName} from {cust.City}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpDelete("{id}")]

        //Asiakkaan poistaminen

        public ActionResult DeleteCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok($"Customer {asiakas.CompanyName} removed");
                }
                else
                {
                    return NotFound("Asiakas idllä" + id + "ei löytynyt");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
            

        }
    }
}
