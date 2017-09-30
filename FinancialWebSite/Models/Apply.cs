using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Apply
    {
        [Key]
        public int ApplyID { get; set; }
        public int UserID { get; set; }
        public DateTime ApplyDate { get; set; }
        public String Reason { get; set; }
        public Boolean IsDeal { get; set; }
    }
}