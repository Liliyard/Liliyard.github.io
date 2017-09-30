using FinancialWebSite.Models;
using FinancialWebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Filters;
using System.Net;
using System.Configuration;
using FinancialWebSite.BusinessLayer;
using System.Web.UI;
using System.IO;
using System.Data.Entity;

namespace FinancialWebSite.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult SaveEdit(Account a, String BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "保存":
                    AccountBusinessLayer accBal = new AccountBusinessLayer();
                    string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
                    a.UserID = int.Parse(s);
                    accBal.EditItem(a);
                    return RedirectToAction("Accounting");
                case "取消":
                    return RedirectToAction("Accounting");
            }
            return new EmptyResult();
            
        }

        // 修改某一项
        [Authorize]
        [HeaderFilter]
        public ActionResult Edit(int id)
        {
            EditViewModel evm = new EditViewModel();
            AccountBusinessLayer actBal = new AccountBusinessLayer();
            Account a = new Account();

            a = actBal.SearchListItem(id);

            evm.AccountID = a.AccountID;
            evm.GenreID = a.GenreID;
            evm.ClassesID = a.ClassesID;
            evm.Money = a.Money;
            evm.Date = a.Date.ToString("yyyy-MM-dd");
            evm.Remark = a.Remark;
            evm.ClassIndex = actBal.GetClassIndex(a.GenreID, a.ClassesID) + 1;

            List<Genre> types = actBal.GetTypeList();//从数据库中获取数据
            ViewBag.ListTypes = types;

            return View("Edit", evm);
        }


        // 删除某一项
        public ActionResult Delete(int id)
        {
            AccountBusinessLayer actBal = new AccountBusinessLayer();
            FinancialERPDAL db = new FinancialERPDAL();
            var delItem = db.Accounts.FirstOrDefault(m => m.AccountID == id);
            if (delItem != null)
            {
                db.Accounts.Remove(delItem);
                db.SaveChanges();
            }
            return RedirectToAction("Accounting");
        }

        public JsonResult GetAllClassByGenreID(int id)
        {
            AccountBusinessLayer abl = new AccountBusinessLayer();
            List<Classes> lstClasses = new List<Classes>();

            Classes c = new Classes();
            c.ClassesID = 0;
            c.GenreID = 0;
            c.Cname = "全部分类";

            if (id != 0)
            {
                lstClasses = abl.GetClassList(id);
            }
            lstClasses.Insert(0, c);
            return Json(lstClasses, JsonRequestBehavior.AllowGet);
        }

        [HeaderFilter]
        public ActionResult SearchAccountListByMonth(DateTime date1, DateTime date2)
        {
            AccountListViewModel accountListViewModel = new AccountListViewModel();
            AccountBusinessLayer actBal = new AccountBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            List<Account> accounts = actBal.SearchList(int.Parse(s), date1, date2);

            accountListViewModel.Accounts = AccountSealed(accounts);
            accountListViewModel.Today = DateTime.Today.ToString("yyyy-MM-dd");

            List<Genre> types = actBal.GetTypeList();//从数据库中获取数据
            ViewBag.ListTypes = types;

            return View("AccountList", accountListViewModel);
        }

        [HeaderFilter]
        public ActionResult SearchAccountListByType(int GenreID, int ClassesID)
        {
            AccountListViewModel accountListViewModel = new AccountListViewModel();
            AccountBusinessLayer actBal = new AccountBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            List<Account> accounts = new List<Account>();
            if (ClassesID != 0)
                accounts = actBal.SearchList(int.Parse(s), GenreID, ClassesID);
            else
                accounts = actBal.SearchList(int.Parse(s), GenreID);

            accountListViewModel.Accounts = AccountSealed(accounts);
            accountListViewModel.Today = DateTime.Today.ToString("yyyy-MM-dd");

            List<Genre> types = actBal.GetTypeList();//从数据库中获取数据
            ViewBag.ListTypes = types;

            return View("AccountList", accountListViewModel);
        }

        

        public ActionResult SaveAccount(Account a, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "保存":
                    AccountBusinessLayer accBal = new AccountBusinessLayer();
                    string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
                    a.UserID = int.Parse(s);
                    accBal.SaveAccount(a);
                    return RedirectToAction("Accounting");
                case "取消":
                    return RedirectToAction("Accounting");
            }
            return new EmptyResult();
        }

        [HeaderFilter]
        public ActionResult AddAccount()
        {
            BaseViewModel bvm = new BaseViewModel();
            AccountBusinessLayer actBal = new AccountBusinessLayer();
            List<Genre> types = actBal.GetTypeList();//从数据库中获取数据
            ViewBag.ListTypes = types;
            return View("AddAccount", bvm);
        }

        // 把Account封装成AccountList
        public List<AccountViewModel> AccountSealed(List<Account> accounts)
        {
            List<AccountViewModel> actViewModels = new List<AccountViewModel>();
            AccountBusinessLayer actBal = new AccountBusinessLayer();

            foreach (Account act in accounts)
            {
                AccountViewModel actViewModel = new AccountViewModel();

                actViewModel.AccountID = act.AccountID;
                actViewModel.UserID = act.UserID;
                actViewModel.Genre = actBal.SearchType(act.GenreID);
                actViewModel.Classes = actBal.SearchClass(act.ClassesID);
                actViewModel.Money = act.Money.ToString("C");
                actViewModel.Date = act.Date.ToString("yyyy-MM-dd");
                actViewModel.Remark = act.Remark;

                if (actViewModel.Genre == "收入")
                {
                    actViewModel.TypeColor = "green";
                }
                else
                {
                    actViewModel.TypeColor = "red";
                }
                actViewModels.Add(actViewModel);

            }
            return actViewModels;
        }

        


        // GET: Account
        [HeaderFilter]
        public ActionResult Accounting()
        {
            AccountListViewModel accountListViewModel = new AccountListViewModel();
            AccountBusinessLayer actBal = new AccountBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            List<Account> accounts = actBal.SearchList(int.Parse(s));

            accountListViewModel.Accounts = AccountSealed(accounts);
            accountListViewModel.Today = DateTime.Today.ToString("yyyy-MM-dd");

            List<Genre> types = actBal.GetTypeList();//从数据库中获取数据
            ViewBag.ListTypes = types;

            return View("AccountList", accountListViewModel);
        }
    }
}