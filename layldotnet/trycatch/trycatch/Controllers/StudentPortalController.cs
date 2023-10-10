using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace trycatch.Controllers
{
    public class Employee
    {
        public string id = "1", name = "jhonrell", age = "18";

        public string printDetails()
        {
            return id + Environment.NewLine + name + Environment.NewLine + age;
        }
    }

    public class StudentPortalController : ApiController
    {
        [Route("api/user/details", Name = "getById")]
        public string getById(string id)
        {
            if (id == "1")
            {
                Employee One = new Employee();
                return One.printDetails();
            }
            else
            {
                return "User not found";
            }
        }
    }
}
