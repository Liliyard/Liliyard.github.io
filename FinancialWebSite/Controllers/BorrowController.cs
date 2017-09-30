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
    public class BorrowController : Controller
    {
        // 搜索
        [HeaderFilter]
        public ActionResult SearchBorrow(String SearchKey, Boolean haveFinish)
        {            
            
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();
            BorrowListViewModel bvml = new BorrowListViewModel();

            List<Borrow> b = bbl.SearchBorrowList(SearchKey, haveFinish);
            bvml.borrows = BorrowSealed(b);
            if (haveFinish)
                return View("HaveFinish", bvml);
            else
                return View("Borrow", bvml);
        }

        // 保存新增
        public ActionResult SaveAdd(Borrow b, String BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "保存":
                    BorrowBusinessLayer bbl = new BorrowBusinessLayer();
                    string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
                    b.UserID = int.Parse(s);
                    b.BFinish = false;
                    bbl.SaveBorrow(b);
                    return RedirectToAction("Borrow");
                case "取消":
                    return RedirectToAction("Borrow");
            }
            return new EmptyResult();
        }

        // 新增
        [HeaderFilter]
        public ActionResult AddNew()
        {
            BaseViewModel bvm = new BaseViewModel();
            return View("AddNew", bvm);
        }

        // 删除
        public ActionResult Delete(int id)
        {
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();
            if(bbl.GetBorrow(id).BFinish)
            {
                bbl.DeleteBorrowItem(id);
                return RedirectToAction("HaveFinish");
            }
            bbl.DeleteBorrowItem(id);
            return RedirectToAction("Borrow");
        }

        // 保存编辑
        public ActionResult SaveEdit(Borrow b, String BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "保存":
                    BorrowBusinessLayer bbl = new BorrowBusinessLayer();
                    bbl.EditItem(b);
                    return RedirectToAction("Borrow");
                case "取消":
                    return RedirectToAction("Borrow");
            }
            return new EmptyResult();
        }

        // 编辑界面
        [HeaderFilter]
        public ActionResult Edit(int id)
        {
            EditBorrowViewModel ebvm = new EditBorrowViewModel();
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();

            Borrow b = bbl.GetBorrow(id);

            ebvm.BorrowID = b.BorrowID;
            ebvm.BTitle = b.BTitle;
            ebvm.BTotal = b.BTotal;
            ebvm.BMonthShouldPay = b.BMonthShouldPay;
            ebvm.BNextRepay = b.BNextRepay.ToShortDateString();
            ebvm.BShouldPay = b.BShouldPay;
            ebvm.BHavePay = b.BHavePay;
            ebvm.BRemark = b.BRemark;

            return View("Edit", ebvm);
        }

        // 还款
        public ActionResult SetBorrowState(int id)
        {
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();
            Borrow b = new Borrow();

            b = bbl.GetBorrow(id);
            if(b.BFinish == false)
            {                
                b.BHavePay += 1;
                if (b.BHavePay == b.BShouldPay)
                    b.BFinish = true;             
                else     
                    b.BNextRepay = b.BNextRepay.AddMonths(1);
                bbl.ChangeItemState(b);
            }            
            return RedirectToAction("Borrow"); 
        }

        // 判断颜色，本月已还为绿色，逾期为红色，应还未到期为黄色
        public String CompareMonth(DateTime today, DateTime next, Boolean BFinish)
        {
            if (BFinish)
                return "#E9E7EF";
            if(DateTime.Compare(today, next) > 0)  // 逾期
                return "#F9906F";
            else
            {
                if (today.Year == next.Year && today.Month == next.Month)
                    return "#FFF143";
                else
                    return "#BCE672";
            }
        }

        // 封装BorrowViewModel
        public List<BorrowViewModel> BorrowSealed(List<Borrow> b)
        {
            List<BorrowViewModel> bvm = new List<BorrowViewModel>();
            DateTime today = DateTime.Today;
            foreach(Borrow item in b)
            {
                BorrowViewModel tmp = new BorrowViewModel();
                tmp.BTitle = item.BTitle;
                tmp.BorrowID = item.BorrowID;
                tmp.UserID = item.UserID;
                tmp.BTotal = item.BTotal.ToString("C");
                tmp.BMonthShouldPay = item.BMonthShouldPay.ToString("C");
                tmp.BNextRepay = item.BNextRepay.ToShortDateString();
                tmp.BShouldPay = item.BShouldPay;
                tmp.BHavePay = item.BHavePay;
                tmp.BRemark = item.BRemark;
                tmp.Bcolor = CompareMonth(today, item.BNextRepay, item.BFinish);
                bvm.Add(tmp);
            }
            return bvm;
        }

        // 已还款
        [HeaderFilter]
        public ActionResult HaveFinish()
        {
            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            BorrowListViewModel bvml = new BorrowListViewModel();
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();

            List<Borrow> b = bbl.GetFinishBorrowList(UID);
            bvml.borrows = BorrowSealed(b);

            return View("HaveFinish", bvml);
        }

        // 未还款
        // GET: Borrow
        [HeaderFilter]
        public ActionResult Borrow()
        {
            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int UID = int.Parse(s);

            BorrowListViewModel bvml = new BorrowListViewModel();
            BorrowBusinessLayer bbl = new BorrowBusinessLayer();

            List<Borrow> b = bbl.GetBorrowList(UID);
            bvml.borrows = BorrowSealed(b);

            return View("Borrow", bvml);
        }
    }
}