using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        public String Aname { get; set; }
        public String Apassword { get; set; }
    }
}