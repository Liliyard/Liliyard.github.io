using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class OperateViewModel
    {
        public int OperateID { get; set; }
        public String Aname { get; set; }
        public String UName { get; set; }
        public DateTime OperateDate { get; set; }
        public String OperateState { get; set; }
    }
}