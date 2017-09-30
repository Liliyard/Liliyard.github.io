using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class TitleViewModel
    {
        public int TitleID { get; set; }
        public String UName { get; set; }
        public String TName { get; set; }
        public DateTime TDate { get; set; }
        public int Reply { get; set; }
    }
}