using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Title
    {
        [Key]
        public int TitleID { get; set; }
        public int UserID { get; set; }
        public String TName { get; set; }
        public DateTime TDate { get; set; }
    }
}