using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class DiaryViewModel
    {
        public int DiaryID { get; set; }
        public String Aname { get; set; }
        public String UName { get; set; }
        public DateTime DiaryDate { get; set; }
        public String ApplyState { get; set; }
    }
}