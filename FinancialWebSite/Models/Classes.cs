using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Classes
    {
        [Key]
        public int ClassesID { get; set; }
        public int GenreID { get; set; }
        public String Cname { get; set; }
    }
}