using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinancialWebSite.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public String UPhone { get; set; }
        public String UName { get; set; }
        public String UPassword { get; set; }
        public Boolean UAuthority { get; set; }
    }
}