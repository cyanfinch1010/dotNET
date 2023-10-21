using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Data;

namespace estupidyante.Controllers
{
    public class EmployeeController : ApiController
    {
        private HttpResponseMessage response;
        DataSet ds = new DataSet();
        //LoginRes res = new LoginRes();

        //[HttpPost] // Change to POST because you are sending sensitive data
        //[Route("api/employee/account/login", Name = "Post_Employee_Login")]
        //public IHttpActionResult Post_Employee_Login([FromBody] LoginRes loginRequest)
        //{
        //    // Validate and process the loginRequest
        //    // ...

        //    // Return a response using the LoginResponse 

        //    if (loginRequest)
        //    {
        //        res.StatusCode = HttpStatusCode.OK;
        //        res.Message = "Login Successful";
        //    }
        //    else
        //    {
        //        res.StatusCode = HttpStatusCode.BadRequest;
        //        res.Message = "Login Failed.";
        //    }

        //    return Ok(res);

        //}



        //[HttpGet]
        //[Route("api/employee/account/login", Name = "Get_Employee_Login")]
        //public HttpResponseMessage Get_Employee_Login(string username, string userpassword, int emp_id)
        //{

        //    using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
        //    {
        //        try
        //        {
        //            if (SQLCON.State == ConnectionState.Closed)
        //            {
        //                SQLCON.Open();
        //                MySqlCommand sqlComm = new MySqlCommand();
        //                sqlComm.Connection = SQLCON;
        //                sqlComm.CommandText = "SELECT COUNT(*) FROM `disciplinepolicy`.`employee` WHERE `emp_Username` = @emp_Username AND `emp_Userpassword` = @emp_Userpassword AND `emp_id` = @emp_id";
        //                sqlComm.Parameters.Add(new MySqlParameter("@emp_Username", username));
        //                sqlComm.Parameters.Add(new MySqlParameter("@emp_Userpassword", userpassword));
        //                sqlComm.Parameters.Add(new MySqlParameter("@emp_id", emp_id));

        //                int count = Convert.ToInt32(sqlComm.ExecuteScalar());
        //                SQLCON.Close();

        //                if (count == 1)
        //                {
        //                    SQLCON.Open();
        //                    DateTime now = DateTime.Now;
        //                    string loginDate = now.ToString("yyyy-MM-dd");
        //                    string loginTime = now.ToString("HH:mm:ss");
        //                    sqlComm.CommandText = "INSERT INTO `disciplinepolicy`.`login_logs`(`emp_id`, `login_date`, `login_time`) VALUES(@emp_id1, @login_date, @login_time)";
        //                    sqlComm.Parameters.Add(new MySqlParameter("@emp_id1", emp_id));
        //                    sqlComm.Parameters.Add(new MySqlParameter("@login_date", loginDate));
        //                    sqlComm.Parameters.Add(new MySqlParameter("@login_time", loginTime));
        //                    sqlComm.ExecuteNonQuery();

        //                    response = Request.CreateResponse(HttpStatusCode.OK);
        //                    response.Content = new StringContent("Login Successful");
        //                    return response;
        //                }
        //                else
        //                {
        //                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
        //                    response.Content = new StringContent("Login Failed.");
        //                    return response;
        //                }

        //            }
        //            else
        //            {
        //                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
        //                response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);
        //                return response;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.OK);
        //            response.Content = new StringContent(ex.ToString(), Encoding.UTF8);
        //            return response;
        //        }
        //        finally
        //        {
        //            SQLCON.Close();
        //            SQLCON.Dispose();
        //        }
        //    }
        //}

        //[Route("api/employee/list", Name = "Get_Employee_List")]
        //public HttpResponseMessage Get_Employee_List()
        //{
        //    using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
        //    {
        //        try
        //        {
        //            if (SQLCON.State == ConnectionState.Closed)
        //            {
        //                SQLCON.Open();

        //                MySqlDataReader dtReader;
        //                MySqlCommand sqlComm = new MySqlCommand();
        //                sqlComm.Connection = SQLCON;
        //                sqlComm.CommandText = "SELECT * FROM employee";

        //                dtReader = sqlComm.ExecuteReader();
        //                if (dtReader.HasRows)
        //                {
        //                    dtReader.Close();
        //                    dtReader = null;
        //                    MySqlDataAdapter sqlDataAdapt = new MySqlDataAdapter(sqlComm);
        //                    sqlDataAdapt.Fill(ds);
        //                    ds.Tables[0].TableName = "Employee_List";
        //                    sqlDataAdapt.Dispose();
        //                    sqlComm.Dispose();
        //                    response = Request.CreateResponse(HttpStatusCode.OK);
        //                    response.Content = new StringContent(JsonConvert.SerializeObject(ds));
        //                    return response;
        //                }
        //                else
        //                {
        //                    response = Request.CreateResponse(HttpStatusCode.NotFound);
        //                    response.Content = new StringContent("Unable to retrieve employee information");
        //                    return response;
        //                }
        //            }
        //            else
        //            {
        //                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
        //                response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);
        //                return response;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.OK);
        //            response.Content = new StringContent(ex.ToString(), Encoding.UTF8);
        //            return response;
        //        }
        //        finally
        //        {
        //            SQLCON.Close();
        //            SQLCON.Dispose();
        //        }
        //    }
        //}

        [HttpGet]
        [Route("api/employee/list", Name = "Get_Employee_List")]
        public IHttpActionResult Get_Employee_List()
        {
            List<modEmployeeList> stats = new List<modEmployeeList>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT * FROM employee", sqlConn))
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                if (dtReader.HasRows)

                                {

                                    while (dtReader.Read())

                                    {

                                        modEmployeeList dataObj = new modEmployeeList();
                                        dataObj.emp_id = dtReader["emp_id"].ToString();

                                        dataObj.emp_lastName = dtReader["emp_last_name"].ToString();
                                        dataObj.emp_firstName = dtReader["emp_first_name"].ToString();
                                        dataObj.emp_email = dtReader["emp_email"].ToString();
                                        dataObj.emp_gender = dtReader["emp_gender"].ToString();
                                        dataObj.emp_accountStatus = dtReader["emp_status"].ToString();

                                        stats.Add(dataObj);

                                    }

                                    return Ok(stats);

                                }
                                else
                                {

                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {

                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        [HttpGet]  
        [Route("api/employee/details", Name = "Get_Employee_Details")]
        public IHttpActionResult Get_Employee_Details(string emp_id)
        {
            List<modEmployeeList> stats = new List<modEmployeeList>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();

                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT * FROM employee WHERE emp_id = @emp_id LIMIT 1", sqlConn))

                        {
                            msqlcom.Parameters.Add(new MySqlParameter("@emp_id", emp_id));
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())

                            {

                                if (dtReader.HasRows)
                                {
                                    while (dtReader.Read())

                                    {

                                        modEmployeeList dataObj = new modEmployeeList();
                                        dataObj.emp_id = dtReader["emp_id"].ToString();
                                        dataObj.emp_lastName = dtReader["emp_last_name"].ToString();
                                        dataObj.emp_firstName = dtReader["emp_first_name"].ToString();
                                        dataObj.emp_email = dtReader["emp_email"].ToString();
                                        dataObj.emp_gender = dtReader["emp_gender"].ToString();
                                        dataObj.emp_Username = dtReader["emp_Username"].ToString();
                                        dataObj.emp_Userpassword = dtReader["emp_Userpassword"].ToString();
                                        dataObj.emp_accountStatus = dtReader["emp_status"].ToString();

                                        stats.Add(dataObj);
                                    }

                                    return Ok(stats);

                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {

                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        //CLASS MODEL FOR THE EMPLOYEE LIST
        public class modEmployeeList
        {
            public string emp_id { get; set; }
            public string emp_lastName { get; set; }
            public string emp_firstName { get; set; }
            public string emp_email { get; set; }
            public string emp_gender { get; set; }
            public string emp_Username { get; set; }
            public string emp_Userpassword { get; set; }
            public string emp_accountStatus { get; set; }
        }

        [HttpPost]
        [Route("api/employee/add", Name = "Post_Employee_Add")]
        public HttpResponseMessage Post_Employee_Add([FromUri] modEmployeeList p)
        {
            using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    if (SQLCON.State == ConnectionState.Closed)

                    {
                        SQLCON.Open();

                        MySqlCommand sqlComm = new MySqlCommand();

                        sqlComm.Connection = SQLCON;

                        sqlComm.CommandText = "INSERT INTO `disciplinepolicy`.`employee`(`emp_id`, `emp_last_name`, `emp_first_name`, `emp_email`, `emp_gender`, `emp_Username`, `emp_Userpassword`, `emp_status`) VALUES(@emp_id, @emp_last_name, @emp_first_name, @emp_email, @emp_gender, @emp_Username, @emp_Userpassword, 'Active')";

                        sqlComm.Parameters.Add(new MySqlParameter("@emp_id", p.emp_id));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_last_name", p.emp_lastName));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_first_name", p.emp_firstName));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_email", p.emp_email));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_gender", p.emp_gender));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_Username", p.emp_Username));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_Userpassword", p.emp_Userpassword));
                        sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING
                        response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent("Successfully Saved");

                        return response;
                    }
                    else
                    {

                        response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                        response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                    response.Content = new StringContent("There is an error in performing this action: " + ex.ToString(), Encoding.Unicode);

                    return response;
                }
                finally //ALWAYS CLOSE AND DISPOSE THE CONNECTION AFTER USING
                {
                    SQLCON.Close();
                    SQLCON.Dispose();

                }
            }
        }

        [HttpPut]
        [Route("api/employee/update", Name = "Put_Employee_Update_Details")]
        public HttpResponseMessage Put_Employee_Update_Details([FromUri] modEmployeeList p)
        {
            using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    if (SQLCON.State == ConnectionState.Closed)
                    {
                        SQLCON.Open();

                        MySqlCommand sqlComm = new MySqlCommand();

                        sqlComm.Connection = SQLCON;

                        sqlComm.CommandText = "UPDATE `employee` SET `emp_last_name` = @emp_last_name, `emp_first_name` = @emp_first_name, `emp_email` = @emp_email, `emp_gender` = @emp_gender, `emp_Username` = @emp_Username, `emp_Userpassword` = @emp_Userpassword WHERE `emp_id` = @emp_id LIMIT 1";

                        sqlComm.Parameters.Add(new MySqlParameter("@emp_id", p.emp_id));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_last_name", p.emp_lastName));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_first_name", p.emp_firstName));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_email", p.emp_email));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_gender", p.emp_gender));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_Username", p.emp_Username));
                        sqlComm.Parameters.Add(new MySqlParameter("@emp_Userpassword", p.emp_Userpassword));
                        sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING
                        response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent("Successfully Updated");

                        return response;
                    }
                    else
                    {

                        response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                        response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                    response.Content = new StringContent("There is an error in performing this action: " + ex.ToString(), Encoding.Unicode);

                    return response;
                }
                finally //ALWAYS CLOSE AND DISPOSE THE CONNECTION AFTER USING
                {
                    SQLCON.Close();
                    SQLCON.Dispose();

                }
            }
        }

        [Route("api/employee/switch", Name = "Employee_Switch_Status")]
        public HttpResponseMessage Employee_Switch_Status([FromUri] modEmployeeList p)
        {
            using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    if (SQLCON.State == ConnectionState.Closed)

                    {

                        SQLCON.Open();
                        MySqlCommand sqlComm = new MySqlCommand();
                        sqlComm.Connection = SQLCON;

                        sqlComm.CommandText = "UPDATE `employee` SET `emp_status` =  WHERE `emp_id` = @emp_id LIMIT 1";

                        sqlComm.Parameters.Add(new MySqlParameter("@emp_id", p.emp_id));
                        sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING
                        response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent("Successfully Removed");

                        return response;
                    }
                    else
                    {

                        response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                        response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                    response.Content = new StringContent("There is an error in performing this action: " + ex.ToString(), Encoding.Unicode);

                    return response;
                }
                finally //ALWAYS CLOSE AND DISPOSE THE CONNECTION AFTER USING
                {
                    SQLCON.Close();
                    SQLCON.Dispose();

                }
            }
        }

    }
}
