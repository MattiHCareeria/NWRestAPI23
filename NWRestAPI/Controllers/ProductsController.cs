using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NWRestAPI.Models;

namespace NWRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private NorthwindContext db;

        public ProductsController(NorthwindContext dbparametri)
        {
            db = dbparametri;
        }

        [HttpGet("{id}")]

        //Hakee yhden tuotteen pääavaimella
        public ActionResult GetProductById(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest($"Product not found with the id {id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }


        //Kaikkien tuotteiden haku
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            try
            {
                var products = db.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }


        //Tuotteen haku nimen osalla
        [HttpGet("productname/{pname}")]

        public ActionResult GetByName(string pname)
        {
            try
            {
                var products = db.Products.Where(p => p.ProductName.Contains(pname));

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }


        //Tuotteen lisääminen
        [HttpPost]

        public ActionResult AddNew([FromBody] Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok($"Added new product {product.ProductName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }


        //Tuotteen muokkaaminen
        [HttpPut("{id}")]

        public ActionResult EditProduct(int id, [FromBody] Product product)
        {
            try
            {
                var pro = db.Products.Find(id);

                if (pro != null)
                {
                    pro.ProductName = product.ProductName;
                    pro.UnitPrice = product.UnitPrice;
                    pro.QuantityPerUnit = product.QuantityPerUnit;
                    pro.Discontinued = product.Discontinued;
                    pro.SupplierId = product.SupplierId;
                    pro.CategoryId = product.CategoryId;
                    pro.UnitsOnOrder = product.UnitsOnOrder;
                    pro.ReorderLevel = product.ReorderLevel;
                    pro.ImageLink = product.ImageLink;

                  


                    db.SaveChanges();
                    return Ok("Edited product " + product.ProductName);
                }

                return NotFound("Product not found with the id " + id);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }




        }
        [HttpDelete("{id}")]

        //Tuotteen poistaminen

        public ActionResult DeletePoductById(int id)
        {
            try
            {
                var product = db.Products.Find(id);

                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return Ok($"Product {product.ProductName} removed");
                }
                else
                {
                    return NotFound("Product with an id" + id + "was no found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }


        }
    }
}
