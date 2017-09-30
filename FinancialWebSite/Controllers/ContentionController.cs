using FinancialWebSite.BusinessLayer;
using FinancialWebSite.Data_Access_Layer;
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
    public class TempData
    {
        public int ContentionID { get; set; }
    }

    public class ContentionController : Controller
    {

        public ActionResult SaveApply(Apply a, String BtnSubmit)
        {
            switch(BtnSubmit)
            {
                case "保存":
                    FinancialERPDAL db = new FinancialERPDAL();
                    string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
                    int UID = int.Parse(s);
                    a.ApplyDate = DateTime.Now;
                    a.IsDeal = false;
                    a.UserID = UID;
                    db.Applys.Add(a);
                    db.SaveChanges();
                    ViewData["Message"] = "提交成功！";
                    return RedirectToAction("Title");
                case "取消":
                    return RedirectToAction("Title");
            }
            return new EmptyResult();
        }

        public ActionResult GetDeleteLink(int id)
        {
            if (Convert.ToBoolean(Session["UserAuthority"]))
            {
                TempData t = new TempData();
                t.ContentionID = id;
                return PartialView("DeleteLink", t);
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult Delete(int id)
        {
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();
            int titleID = cbl.GetTitleID(id);

            if (cbl.IsFirstFloor(id))    // 判断是否是一楼
            {              
                cbl.DeleteTitle(titleID);
                return RedirectToAction("Title");
            }
            else
            {
                cbl.DeleteContention(id);
                return Redirect("/Contention/Contention/" + titleID.ToString());
            }
            
        }

        [HeaderFilter]
        public ActionResult SearchTitle(String SearchKey)
        {
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();
            List<Title> t = cbl.SearchTitle(SearchKey);
            TitleListViewModel tvm = new TitleListViewModel();
            tvm.Titles = TitleSeal(t);

            return View("Title", tvm);
        }

        public ActionResult AddTitle(Title t, Contention c)
        {
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);
            t.TDate = DateTime.Now;
            t.UserID = UID;
            c.TitleID = cbl.SaveTitle(t);
            c.UserID = t.UserID;
            c.CDate = t.TDate;
            c.CIsFirstFloor = true;
            cbl.SaveContention(c);
            return RedirectToAction("Title");
        }

        public ActionResult AddContention(int titleID, String Reply)
        {
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            Contention c = new Contention();
            c.CDate = DateTime.Now;
            c.Contentions = Reply;
            c.TitleID = titleID;
            c.UserID = UID;
            c.CIsFirstFloor = false;

            cbl.SaveContention(c);
            return Redirect("/Contention/Contention/" + titleID.ToString());

        }

        public List<TitleViewModel> TitleSeal(List<Title> t)
        {
            List<TitleViewModel> tvml = new List<TitleViewModel>();
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();

            List<ReplyNum> rn = cbl.GetReplyNum();

            foreach(Title item in t)
            {
                TitleViewModel tvm = new TitleViewModel();
                tvm.UName = pbl.GetUserInfo(item.UserID).UName;
                tvm.TName = item.TName;
                tvm.TitleID = item.TitleID;
                tvm.TDate = item.TDate;
                foreach(ReplyNum r in rn)
                {
                    if (r.TitleID == item.TitleID)
                    {
                        tvm.Reply = r.ReplyNumber;
                        break;
                    }                       
                }
                tvml.Add(tvm);
            }
            return tvml;
        }

        public List<ContentionViewModel> ContentionSeal(List<Contention> c)
        {
            List<ContentionViewModel> clvm = new List<ContentionViewModel>();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();

            foreach(Contention item in c)
            {
                ContentionViewModel cvm = new ContentionViewModel();
                cvm.TitleID = item.TitleID;
                cvm.ContentionID = item.ContentionID;
                cvm.UName = pbl.GetUserInfo(item.UserID).UName;
                cvm.Contentions = item.Contentions;
                cvm.CDate = item.CDate.ToString();
                clvm.Add(cvm);
            }
            return clvm;
        }

        [HeaderFilter]
        public ActionResult Apply()
        {
            BaseViewModel bvm = new BaseViewModel();
            return View("Apply", bvm);
        }

        [HeaderFilter]
        public ActionResult Title()
        {
            TitleListViewModel tlvm = new TitleListViewModel();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            List<Title> t = cbl.GetTitleList();
            tlvm.Titles = TitleSeal(t);
            tlvm.Users = abl.GetUser(true);

            return View("Title", tlvm);
        }

        // GET: Content
        [HeaderFilter]
        public ActionResult Contention(int id)
        {
            ContentionListViewModel clvm = new ContentionListViewModel();
            ContentionBusinessLayer cbl = new ContentionBusinessLayer();

            List<Contention> c = cbl.GetContentionList(id);

            clvm.Contents = ContentionSeal(c);
            clvm.Title = cbl.GetTitle(id);

            return View("Contentions", clvm);
        }
    }
}