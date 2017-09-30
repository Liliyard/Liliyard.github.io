using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using FinancialWebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class IndexBusinessLayer
    {
        // 本周第一天
        public DateTime GetWeekFirstDay(DateTime datetime)
        {

            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            DateTime FirstDay = datetime.AddDays(daydiff);

            return FirstDay;
        }

        // 本周最后一天
        public DateTime GetWeekLastDay(DateTime datetime)
        {

            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            DateTime LastDay = datetime.AddDays(daydiff);
            return LastDay;
        }

        public DateTime GetMonthFirstDay(DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1);
        }

        public DateTime GetMonthLastDay(DateTime now)
        {
            DateTime FirstDay = new DateTime(now.Year, now.Month, 1);
            return FirstDay.AddMonths(1).AddDays(-1);
        }

        public DateTime GetYearFirstDay(DateTime now)
        {
            return new DateTime(now.Year, 1, 1);
        }

        public DateTime GetYearLastDay(DateTime now)
        {
            return new DateTime(now.Year, 12, 31);
        }


        public List<AccountClassInOut> GetMonthClassInOut(int UID, int GID, DateTime FirstDay, DateTime LastDay)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            List<AccountClassInOut> ag = new List<AccountClassInOut>();


            var accounts = from m in db.Accounts
                           select m;
            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.Where(s => s.GenreID == GID);
                accounts = accounts.Where(s => s.Date >=FirstDay);
                accounts = accounts.Where(s => s.Date <= LastDay);
            }

            var query = from l in accounts
                        group l by new { l.ClassesID } into g
                        select new
                        {
                            CID = g.Key.ClassesID,
                            Money = g.Sum(a => a.Money)
                        };

            foreach (var q in query)
            {
                AccountClassInOut t = new AccountClassInOut();
                t.ClassesID = q.CID;
                t.Money = q.Money;
                ag.Add(t);
            }

            return ag;

        }

        // 查询今日收支情况
        public List<AccountInOut> GetTodayInOut(int UID, DateTime today)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            List<AccountInOut> ag = new List<AccountInOut>();
            // 初始化
            for (int i = 1; i <= 2; i++)
            {
                AccountInOut t = new AccountInOut();
                t.GenreID = i;
                t.Money = 0M;
                ag.Add(t);
            }


            var accounts = from m in db.Accounts
                           select m;
            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.Where(s => s.Date == today);
            }
            if(accounts.ToList<Account>().Count == 0)
            {                
                return ag;
            }
            var query = from l in accounts
                        group l by new { l.GenreID } into g
                        select new
                        {
                            GID = g.Key.GenreID,
                            Money = g.Sum(a => a.Money)                            
                        };
            
            foreach (var q in query)
            {
                if (q.GID == 1)
                    ag[0].Money = q.Money;
                else
                    ag[1].Money = q.Money;
            }

            return ag;

        }

        public List<AccountInOut> GetTheseDayInOut(int UID, DateTime firstday, DateTime lastday)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            List<AccountInOut> ag = new List<AccountInOut>();
            // 初始化
            for (int i = 1; i <= 2; i++)
            {
                AccountInOut t = new AccountInOut();
                t.GenreID = i;
                t.Money = 0M;
                ag.Add(t);
            }

            var accounts = from m in db.Accounts
                           select m;
            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.Where(s => s.Date >= firstday);
                accounts = accounts.Where(s => s.Date <= lastday);
            }

            if (accounts.ToList<Account>().Count == 0)
            {
                for (int i = 1; i <= 2; i++)
                {
                    AccountInOut t = new AccountInOut();
                    t.GenreID = i;
                    t.Money = 0M;
                    ag.Add(t);
                }
                return ag;
            }

            var query = from l in accounts
                        group l by new { l.GenreID } into g
                        select new
                        {
                            GID = g.Key.GenreID,
                            Money = g.Sum(a => a.Money)
                        };

            foreach (var q in query)
            {
                if (q.GID == 1)
                    ag[0].Money = q.Money;
                else
                    ag[1].Money = q.Money;
            }

            return ag;

        }

    }
}