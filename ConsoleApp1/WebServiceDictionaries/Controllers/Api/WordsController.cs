
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Http.Description;
using WebServiceDictionaries.Models;

namespace WebServiceDictionaries.Controllers.Api
{
    public class WordsController : Controller
    {

        // GET: api/Employees/5
        /*  [ResponseType(typeof(Word))]
          public IHttpActionResult GetApp(string language)//, int? amoutOfWords)
          {
            //  IList<Word> words;
            //  words = NHibernateHelper.GetAllData(language);

             /* if (words == null)
              {
                  return BadRequest();
              }*/
        /*
                    return Ok(2);
                }*/

        // GET: api/Employees
        [ResponseType(typeof(Word))]
        [System.Web.Http.Route("GetWords/{language}")]
        public JsonResult GetWords()
        {
        /* IList<Word> words;// words = new IList<Word>();
         words = NHibernateHelper.GetAllData("pl");
         return words;*/

        return Json(2);
        
        }


        /* private EmployeesContext db = new EmployeesContext();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }*/

    }
}
