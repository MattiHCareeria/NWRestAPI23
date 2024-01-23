using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NWRestAPI.Models;

namespace NWRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Dependency injektio
        private NorthwindContext db;

        public EmployeesController(NorthwindContext dbparametri)
        {
            db = dbparametri;
        }

        [HttpGet("{id}")]

        //Hakee yhen asiakkaan pääavaimella
        public ActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = db.Employees.Find(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return BadRequest($"Employee not found with the id {id}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetAllEployees()
        {
            try
            {
                var employees = db.Employees.ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet("employeename/{ename}")]

        public ActionResult GetByName(string ename)
        {
            try
            {
                var emp = db.Employees.Where(e => e.LastName.Contains(ename));

                return Ok(emp);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        [HttpPost]

        public ActionResult AddNew([FromBody] Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Ok($"Added new employee {emp.LastName} , {emp.FirstName}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPut("{id}")]

        public ActionResult EditEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                var emp = db.Employees.Find(id);

                if (emp != null)
                {
                    emp.LastName = employee.LastName;
                    emp.FirstName = employee.FirstName;
                    emp.Address = employee.Address;
                    emp.City = employee.City;
                    emp.Orders = employee.Orders;
                    emp.TitleOfCourtesy = employee.TitleOfCourtesy;
                    emp.Country = employee.Country;
                    emp.PostalCode = employee.PostalCode;
                    emp.Photo = employee.Photo;
                    emp.PhotoPath = employee.PhotoPath;
                    emp.BirthDate = employee.BirthDate;
                    emp.HireDate = employee.HireDate;
                    emp.Extension = employee.Extension;
                    emp.HomePhone = employee.HomePhone;
                    emp.Notes = employee.Notes;
                    emp.Region = employee.Region;
                    emp.ReportsTo = employee.ReportsTo;
                    emp.Title = employee.Title;
                   

                    db.SaveChanges();
                    return Ok("Edited employee " + employee.LastName + ", " + employee.FirstName);
                }

                return NotFound("Employee not found with the id " + id);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }




        }
        [HttpDelete("{id}")]

        //Asiakkaan poistaminen

        public ActionResult DeleteEmployeeById(int id)
        {
            try
            {
                var employee = db.Employees.Find(id);

                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return Ok($"Employee {employee.LastName}, {employee.FirstName} removed");
                }
                else
                {
                    return NotFound("Employee with an id" + id + "was no found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }


        }
    }
}
