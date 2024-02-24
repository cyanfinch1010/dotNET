using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webAppLayla.Models;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;

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

        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(modHome p)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "emp_id=" + p.emp_id
                        + "&emp_lastName=" + p.emp_lastName
                        + "&emp_firstName=" + p.emp_firstName
                        + "&emp_email=" + p.emp_email
                        + "&emp_gender=" + p.emp_gender
                        + "&emp_Username=" + p.emp_Username
                        + "&emp_Userpassword=" + p.emp_Userpassword;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/employee/add?" + postData);
                    Byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Successfully Saved");
                        if (comVal == 0)
                        {
                            return Content("Successfully Saved", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        public ActionResult EditEmployee()
        {
            HomeModel mymodel = new HomeModel();
            mymodel.empList = GetEmployeeDetailsbyId();
            return View("EditEmployee", mymodel);
        }
        [HttpPut]
        public ActionResult EditEmployee(modHome p)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string putData = "emp_id=" + p.emp_id
                        + "&emp_lastName=" + p.emp_lastName
                        + "&emp_firstName=" + p.emp_firstName
                        + "&emp_email=" + p.emp_email
                        + "&emp_gender=" + p.emp_gender
                        + "&emp_Username=" + p.emp_Username
                        + "&emp_Userpassword=" + p.emp_Userpassword;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/employee/update?" + putData);
                    Byte[] data = Encoding.UTF8.GetBytes(putData);
                    req.Method = "PUT";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Successfully Updated");
                        if (comVal == 0)
                        {
                            return Content("Successfully Updated", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        public ActionResult ChangeStatus()
        {
            HomeModel mymodel = new HomeModel();
            mymodel.empList = GetEmployeeDetailsbyId();
            return View("ChangeStatus", mymodel);
        }
        [HttpDelete]
        public ActionResult ChangeStatus(modHome p)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string putData = "emp_id=" + p.emp_id
                        + "&emp_accountStatus=" + p.emp_accountStatus;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/employee/switch?" + putData);
                    Byte[] data = Encoding.UTF8.GetBytes(putData);
                    req.Method = "DELETE";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Successfully Updated Status");
                        if (comVal == 0)
                        {
                            return Content("Successfully Updated Status", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(modHome p)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&emp_Username=" + p.emp_Username
                        + "&emp_Userpassword=" + p.emp_Userpassword;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/employee/account/login?" + postData);
                    Byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Successfully Logged in");
                        if (comVal == 0)
                        {
                            return Content("Successfully Logged in", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }
    }
}