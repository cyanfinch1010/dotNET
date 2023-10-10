using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAppLayla.Models
{
    public class modHome
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
    public class HomeModel
    {
        public IEnumerable<modHome> empList { get; set; }
    }
}