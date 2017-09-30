using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        public String ThisYear { get; set; }
        public String Today { get; set; }
        public String WeekFDay { get; set; }
        public String WeekLDay { get; set; }
        public String MonthFDay { get; set; }
        public String MonthLDay { get; set; }

        public decimal TodayIn { get; set; }
        public decimal TodayOut { get; set; }

        public decimal WeekIn { get; set; }
        public decimal WeekOut { get; set; }

        public decimal MonthIn { get; set; }
        public decimal MonthOut { get; set; }

        public decimal YearIn { get; set; }
        public decimal YearOut { get; set; }
    }
}