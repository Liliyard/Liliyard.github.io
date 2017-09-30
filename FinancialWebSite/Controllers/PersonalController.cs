using FinancialWebSite.BusinessLayer;
using FinancialWebSite.Filters;
using FinancialWebSite.Models;
using FinancialWebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialWebSite.Controllers
{
    public class PersonalController : Controller
    {

        [HeaderFilter]
        public ActionResult SearchTitle(String SearchKey)
        {
            ContentionController cc = new ContentionController();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();
            List<Title> t = cbl.SearchTitle(SearchKey);
            TitleListViewModel tvm = new TitleListViewModel();
            tvm.Titles = cc.TitleSeal(t);

            return View("MyContention", tvm);
        }

        [HeaderFilter]
        public ActionResult MyContention()
        {
            ContentionController cc = new ContentionController();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            List<Title> t = cbl.GetTitleList(UID);
            TitleListViewModel tvm = new TitleListViewModel();
            tvm.Titles = cc.TitleSeal(t);

            return View("MyContention", tvm);
        }

        public Boolean doChangePassword(String OriPassword, String NewPassword)
        {
            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            PersonalBusinessLayer pbl = new PersonalBusinessLayer();
            if (pbl.GetUPassword(UID).Equals(OriPassword))
            {
                pbl.SavePassword(UID, NewPassword);
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public ActionResult ChangePassword(String btnSubmit, String OriPassword, String NewPassword)
        {
            switch (btnSubmit)
            {
                case "提交":
                    Boolean state = doChangePassword(OriPassword, NewPassword);
                    return RedirectToAction("PersonalInfo");
                case "取消":
                    return RedirectToAction("PersonalInfo");
            }
            return new EmptyResult();
        }
    

        // GET: Personal
        [Authorize]
        [HeaderFilter]
        public ActionResult PersonalInfo()
        {
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();
            PersonalInfoViewModel pivm = new PersonalInfoViewModel();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);
            pivm.UserID = UID;
            pivm.UPhone = pbl.GetUPhone(UID);
            return View("PersonalInfo", pivm);
        }
    }
}