using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialWebSite.ViewModels;
using FinancialWebSite.Filters;
using FinancialWebSite.BusinessLayer;
using FinancialWebSite.Models;
using System.Web.Script.Serialization;

namespace FinancialWebSite.Controllers
{


    public class IndexController : Controller
    {

        

        // 获取本月收支情况，细分
        public JsonResult GetMonthClassGroup(int id)
        {
            IndexBusinessLayer ibl = new IndexBusinessLayer();
            AccountBusinessLayer abl = new AccountBusinessLayer();
            List<AccountClassInOutViewModel> acvm = new List<AccountClassInOutViewModel>();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            DateTime today = DateTime.Today;
            DateTime date1 = ibl.GetMonthFirstDay(today);
            DateTime date2 = ibl.GetMonthLastDay(today);
            List<AccountClassInOut> ag = ibl.GetMonthClassInOut(UID, id, date1, date2);

            foreach(var item in ag)
            {
                AccountClassInOutViewModel a = new AccountClassInOutViewModel();
                a.Cname = abl.SearchClass(item.ClassesID);
                a.Money = item.Money;
                acvm.Add(a);
            }

            return Json(acvm, JsonRequestBehavior.AllowGet);
        }

        // 每月收支情况
        public JsonResult GetEveryMonthGroup()
        {
            IndexBusinessLayer ibl = new IndexBusinessLayer();
            DateTime today = DateTime.Today;
            int UID = 0;

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            UID = int.Parse(s);

            List<YearInOutViewModel> ag = new List<YearInOutViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                List<AccountInOut> a = new List<AccountInOut>();
                YearInOutViewModel y = new YearInOutViewModel();
                DateTime first = new DateTime(today.Year, i, 1);
                DateTime last = first.AddMonths(1).AddDays(-1);
                a = ibl.GetTheseDayInOut(UID, first, last);
                //a = ibl.GetTheseDayInOut(1, first, last);
                y.label = i.ToString() + "月";
                y.MonthIn = a[0].Money;
                y.MonthOut = a[1].Money;
                ag.Add(y);
            }

            return Json(ag, JsonRequestBehavior.AllowGet);
        }

        // GET: Index
        [Authorize]
        [HeaderFilter]
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();
            IndexBusinessLayer ibl = new IndexBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);
            DateTime today = DateTime.Today;

            ivm.ThisYear = DateTime.Now.Year.ToString();
            ivm.Today = DateTime.Now.ToString("yyyy年MM月dd日");
            ivm.WeekFDay = ibl.GetWeekFirstDay(today).ToString("MM月dd日");
            ivm.WeekLDay = ibl.GetWeekLastDay(today).ToString("MM月dd日");
            ivm.MonthFDay = ibl.GetMonthFirstDay(today).ToString("MM月dd日");
            ivm.MonthLDay = ibl.GetMonthLastDay(today).ToString("MM月dd日");

            List<AccountInOut> todayInOut = ibl.GetTodayInOut(UID, today);
            ivm.TodayIn = todayInOut[0].Money;   // 1收入2支出
            ivm.TodayOut = todayInOut[1].Money;

            List<AccountInOut> thisWeekInOut = ibl.GetTheseDayInOut(UID, ibl.GetWeekFirstDay(today), ibl.GetWeekLastDay(today));
            ivm.WeekIn = thisWeekInOut[0].Money;   // 1收入2支出
            ivm.WeekOut = thisWeekInOut[1].Money;

            List<AccountInOut> thisMonthInOut = ibl.GetTheseDayInOut(UID, ibl.GetMonthFirstDay(today), ibl.GetMonthLastDay(today));
            ivm.MonthIn = thisMonthInOut[0].Money;   // 1收入2支出
            ivm.MonthOut = thisMonthInOut[1].Money;

            List<AccountInOut> thisYearInOut = ibl.GetTheseDayInOut(UID, ibl.GetYearFirstDay(today), ibl.GetYearLastDay(today));
            ivm.YearIn = thisYearInOut[0].Money;   // 1收入2支出
            ivm.YearOut = thisYearInOut[1].Money;

            return View("Index", ivm);
        }
    }
}