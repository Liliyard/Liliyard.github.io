using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.ViewModels
{
    public class YearInOutViewModel
    {
        public String label { get; set; }  // 月份
        public decimal MonthIn { get; set; }
        public decimal MonthOut { get; set; }
    }
}