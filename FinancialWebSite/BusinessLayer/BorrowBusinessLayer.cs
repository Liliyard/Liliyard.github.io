using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class BorrowBusinessLayer
    {
        public void EditItem(Borrow b)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Borrow tmp = new Borrow();
            tmp = (from m in db.Borrows
                   where m.BorrowID == b.BorrowID
                   select m).Single<Borrow>();
            tmp.BTitle = b.BTitle;
            tmp.BTotal = b.BTotal;
            tmp.BMonthShouldPay = b.BMonthShouldPay;
            tmp.BNextRepay = b.BNextRepay;
            tmp.BShouldPay = b.BShouldPay;
            tmp.BHavePay = b.BHavePay;
            tmp.BRemark = b.BRemark;
            tmp.BFinish = b.BFinish;

            db.SaveChanges();
        }

        public void ChangeItemState(Borrow b)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Borrow tmp = new Borrow();
            tmp = (from m in db.Borrows
                   where m.BorrowID == b.BorrowID
                   select m).Single<Borrow>();
            tmp.BNextRepay = b.BNextRepay;
            tmp.BHavePay = b.BHavePay;
            tmp.BFinish = b.BFinish;

            db.SaveChanges();
        }


        public Borrow GetBorrow(int BID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var borrows = from m in db.Borrows
                          select m;
            if(BID != 0)
            {
                borrows = borrows.Where(s => s.BorrowID == BID);
            }
            return borrows.FirstOrDefault<Borrow>();
        }

        public List<Borrow> GetBorrowList(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var borrows = from m in db.Borrows
                          select m;
            if(UID != 0)
            {
                borrows = borrows.Where(s => s.UserID == UID);
                borrows = borrows.Where(s => s.BFinish == false);
                borrows = borrows.OrderBy(s => s.BNextRepay);
            }
            return borrows.ToList<Borrow>();
        }

        public List<Borrow> GetFinishBorrowList(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var borrows = from m in db.Borrows
                          select m;
            if (UID != 0)
            {
                borrows = borrows.Where(s => s.UserID == UID);
                borrows = borrows.Where(s => s.BFinish == true);
                borrows = borrows.OrderBy(s => s.BNextRepay);
            }
            return borrows.ToList<Borrow>();
        }

        public void DeleteBorrowItem(int id)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var delItem = db.Borrows.FirstOrDefault(m => m.BorrowID == id);
            if (delItem != null)
            {
                db.Borrows.Remove(delItem);
                db.SaveChanges();
            }
        }

        // 搜索借贷信息
        public List<Borrow> SearchBorrowList(String SearchKey, Boolean fin)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var borrows = from m in db.Borrows
                          select m;
            if (!String.IsNullOrEmpty(SearchKey))
            {
                borrows = borrows.Where(s => s.BTitle.Contains(SearchKey));
                borrows = borrows.Where(s => s.BFinish == fin);
                borrows = borrows.OrderBy(s => s.BNextRepay);
            }
            return borrows.ToList<Borrow>();
        }

        public Borrow SaveBorrow(Borrow b)
        {
            FinancialERPDAL finanDal = new FinancialERPDAL();
            finanDal.Borrows.Add(b);
            finanDal.SaveChanges();
            return b;
        }

    }
}