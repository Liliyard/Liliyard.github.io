using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialWebSite.Models
{
    public class Operate
    {
        [Key]
        public int OperateID { get; set; }
        public int AdminID { get; set; }
        public int UserID { get; set; }
        public DateTime OperateDate { get; set; }
        public String OperateState { get; set; }
    }
}