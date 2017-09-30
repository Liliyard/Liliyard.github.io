using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class AccountBusinessLayer
    {
        // 保存新增数据
        public Account SaveAccount(Account a)
        {
            FinancialERPDAL finanDal = new FinancialERPDAL();
            finanDal.Accounts.Add(a);
            finanDal.SaveChanges();
            return a;
        }

        // 查询某一项账单
        public Account SearchListItem(int AccID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var accounts = from m in db.Accounts
                           select m;

            if (AccID != 0)
            {
                accounts = accounts.Where(s => s.AccountID == AccID);
            }

            return accounts.FirstOrDefault<Account>();
        }


        /*       获取、查询账单目录      */
        // 通过用户ID查询目录
        public List<Account> SearchList(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var accounts = from m in db.Accounts
                           select m;

            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.OrderByDescending(m => m.AccountID).OrderByDescending(s => s.Date);
            }
            
            return accounts.ToList<Account>();
        }
        // 通过类型查询目录
        public List<Account> SearchList(int UID, int TypeID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var accounts = from m in db.Accounts
                           select m;
            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.OrderByDescending(m => m.AccountID).OrderByDescending(s => s.Date);
            }
            if(TypeID != 0)
            {
                accounts = accounts.Where(s => s.GenreID == TypeID);
            }

            return accounts.ToList<Account>();
        }
        // 通过分类查询目录
        public List<Account> SearchList(int UID, int TypeID, int ClassID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var accounts = from m in db.Accounts
                           select m;

            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.OrderByDescending(m => m.AccountID).OrderByDescending(s => s.Date);
            }
            if(ClassID != 0)
            {
                accounts = accounts.Where(s => s.ClassesID == ClassID);
            }

            return accounts.ToList<Account>();
        }
        // 通过日期范围查询目录
        public List<Account> SearchList(int UID, DateTime date1, DateTime date2)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var accounts = from m in db.Accounts
                           select m;

            if (UID != 0)
            {
                accounts = accounts.Where(s => s.UserID == UID);
                accounts = accounts.OrderByDescending(m => m.AccountID).OrderByDescending(s => s.Date);
            }
            if(date1 != DateTime.MinValue)
            {
                accounts = accounts.Where(s => s.Date >= date1);
            }
            if(date1 < date2)
            {
                accounts = accounts.Where(s => s.Date <= date2);
            }

            return accounts.ToList<Account>();
        }


        /*           查询类型(收入、支出)             */
        // 获取类名
        public String SearchType(int TID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Genre g = new Genre();
            var genres = from m in db.Genres
                         select m;
            if (TID != 0)
            {
                genres = genres.Where(s => s.GenreID == TID);
            }
            g = genres.First<Genre>();
            return g.Gname;
        }
        // 获取类ID
        public int SearchType(String type)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Genre g = new Genre();
            var genres = from m in db.Genres
                         select m;
            if (!String.IsNullOrEmpty(type))
            {
                genres = genres.Where(s => s.Gname.Contains(type));
            }
            g = genres.First<Genre>();
            return g.GenreID;
        }

        /*           查询分类（通讯费、餐费等）          */
        // 获取分类名
        public String SearchClass(int CID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Classes c = new Classes();
            var tclasses = from m in db.TClasses
                           select m;
            if (CID != 0)
            {
                tclasses = tclasses.Where(s => s.ClassesID == CID);
            }
            c = tclasses.First<Classes>();
            return c.Cname;
        }

        // 获取分类ID
        public int SearchClass(String classes)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Classes c = new Classes();
            var tclasses = from m in db.TClasses
                           select m;
            if (!String.IsNullOrEmpty(classes))
            {
                tclasses = tclasses.Where(s => s.Cname.Contains(classes));
            }
            c = tclasses.First<Classes>();
            return c.ClassesID;
        }

        /*  获取分类列表  */
        public List<Classes> GetClassList(int GID)
        { 
            FinancialERPDAL db = new FinancialERPDAL();
            var tclasses = from m in db.TClasses
                           select m;

            if (GID != 0)
            {
                tclasses = tclasses.Where(s => s.GenreID == GID);
            }

            return tclasses.ToList<Classes>();
            
        }

        /*  获取类别列表  */
        public List<Genre> GetTypeList()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            return db.Genres.ToList<Genre>();
        }

        public int GetClassIndex(int GenreID, int ClassesID)
        {
            List<Classes> c = GetClassList(GenreID);
            int index = 0;
            foreach(Classes item in c)
            {
                if (item.ClassesID == ClassesID)
                {
                    index = c.IndexOf(item);
                    break;
                }
                    
            }
            return index;
        }

        public static string DecimalToString(decimal d)
        {
            return d.ToString("#0.##");
        }


        public void EditItem(Account a)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Account tmp = new Account();
            tmp = (from m in db.Accounts
                   where m.AccountID == a.AccountID
                   select m).Single<Account>();
            tmp.GenreID = a.GenreID;
            tmp.ClassesID = a.ClassesID;
            tmp.Money = a.Money;
            tmp.Date = a.Date;
            tmp.Remark = a.Remark;
            db.SaveChanges();
        }

    }
}