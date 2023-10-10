using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webAppLayla.Models;
using System.Net.Http;
using System.Configuration;


namespace webAppLayla.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            HomeModel mymodel = new HomeModel();
            mymodel.empList = employeeList();
            return View("Index", mymodel);
        }
        public IEnumerable<modHome> employeeList()
        {
            IEnumerable<modHome> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/employee/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<modHome>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<modHome>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public ActionResult EmployeeDetails()
        {
            HomeModel mymodel = new HomeModel();
            mymodel.empList = GetEmployeeDetailsbyId();
            return View("EmployeeDetails", mymodel);
        }
        public IEnumerable<modHome> GetEmployeeDetailsbyId()
        {
            IEnumerable<modHome> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/employee/");

            var consumedata = hc.GetAsync("details?emp_id=" + Request.QueryString["emp_id"].ToString());
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<modHome>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<modHome>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }
    }
}