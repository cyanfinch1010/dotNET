using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace estupidyante.Controllers
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }

    public class StudentsController : ApiController
    {
        private Dictionary<int, Student> students = new Dictionary<int, Student>
        {
            {1, new Student {Id = 1, Name = "Layl", Gender = "Male", Age = 18, ContactNumber = "092312345", Email = "layl@redhat.com"}},
        };

        // GET: api/Students
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Students/5
        public IHttpActionResult Get(int id)
        {
            if (students.TryGetValue(id, out Student student))
            {
                return Ok(student);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "student not found.");
            }
        }
        // POST: api/Students
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Students/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Students/5
        public void Delete(int id)
        {
        }
    }
}
