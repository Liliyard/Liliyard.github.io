using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class AdminApplyViewModel
    {
        public int ApplyID { get; set; }
        public int UserID { get; set; }
        public String UName { get; set; }
        public String ApplyDate { get; set; }
        public String Reason { get; set; }
        public int PostingAmount { get; set; }
    }
}