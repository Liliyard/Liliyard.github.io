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
    public class AdministerController : Controller
    {


        public ActionResult AgreeAdmin(int id)
        {
            AdministerBusinessLayer abl = new AdministerBusinessLayer();
            abl.ChangeState(id ,true);
            return RedirectToAction("Apply");
        }

        public ActionResult EscAdmin(int id)
        {
            AdministerBusinessLayer abl = new AdministerBusinessLayer();
            abl.ChangeState(id, false);
            return RedirectToAction("Apply");
        }

        public ActionResult CancelAdmin(int id)
        {
            AdministerBusinessLayer abl = new AdministerBusinessLayer();
            abl.SetAdmin(id, false);
            return RedirectToAction("Manager");
        }

        public List<AdminManageViewModel> AMVMSealed(List<User> u)
        {
            List<AdminManageViewModel> amvml = new List<AdminManageViewModel>();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            foreach(User item in u)
            {
                AdminManageViewModel amvm = new AdminManageViewModel();
                amvm.UserID = item.UserID;
                amvm.UName = item.UName;
                amvm.PostingAmount = abl.GetPostingAmount(item.UserID);
                amvml.Add(amvm);
            }
            return amvml;
        }

        public List<AdminApplyViewModel> AAVMSealed(List<Apply> la)
        {
            List<AdminApplyViewModel> aavml = new List<AdminApplyViewModel>();
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            foreach (Apply a in la)
            {
                AdminApplyViewModel aavm = new AdminApplyViewModel();
                aavm.ApplyID = a.ApplyID;
                aavm.UserID = a.UserID;
                aavm.UName = pbl.GetUserInfo(a.UserID).UName;
                aavm.ApplyDate = a.ApplyDate.ToString();
                aavm.PostingAmount = abl.GetPostingAmount(a.UserID);
                aavm.Reason = a.Reason;

                aavml.Add(aavm);
            }

            return aavml;
        }

        public List<DiaryViewModel> DLVMSealed(List<Diary> diarys)
        {
            List<DiaryViewModel> dvml = new List<DiaryViewModel>();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();

            foreach (Diary item in diarys)
            {
                DiaryViewModel dvm = new DiaryViewModel();
                dvm.Aname = abl.GetAname(item.AdminID);
                dvm.ApplyState = item.ApplyState;
                dvm.DiaryDate = item.DiaryDate;
                dvm.DiaryID = item.DiaryID;
                dvm.UName = pbl.GetUserInfo(abl.SearchApply(item.ApplyID).UserID).UName;
                dvml.Add(dvm);
            }
            return dvml;
        }

        public List<OperateViewModel> OLVMSealed(List<Operate> operates)
        {
            List<OperateViewModel> ovml = new List<OperateViewModel>();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();
            PersonalBusinessLayer pbl = new PersonalBusinessLayer();

            foreach (Operate item in operates)
            {
                OperateViewModel ovm = new OperateViewModel();
                ovm.Aname = abl.GetAname(item.AdminID);
                ovm.OperateDate = item.OperateDate;
                ovm.OperateID = item.OperateID;
                ovm.OperateState = item.OperateState;
                ovm.UName = pbl.GetUserInfo(item.UserID).UName;

                ovml.Add(ovm);
            }
            return ovml;
        }

        [HeaderFilter]
        public ActionResult Apply()
        {
            AdminUserApplyListViewModel aual = new AdminUserApplyListViewModel();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            aual.Applys = AAVMSealed(abl.GetApply());

            return View("AdminUserApply", aual);
        }

        // GET: Administer
        [HeaderFilter]
        public ActionResult Manager()
        {
            AdminUserManageListViewModel auml = new AdminUserManageListViewModel();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            auml.Admins = AMVMSealed(abl.GetUser(true));

            return View("AdminUserManager", auml);
        }

        [HeaderFilter]
        public ActionResult ApplyLog()
        {
            DiaryListViewModel dlvm = new DiaryListViewModel();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            dlvm.Diaries = DLVMSealed(abl.GetDiaryList());

            return View("ApplyLog", dlvm);
        }

        [HeaderFilter]
        public ActionResult OperateLog()
        {
            OperateListViewModel olvm = new OperateListViewModel();
            AdministerBusinessLayer abl = new AdministerBusinessLayer();

            olvm.Operates = OLVMSealed(abl.GetOperateList());

            return View("OperateLog", olvm);
        }

    }
}