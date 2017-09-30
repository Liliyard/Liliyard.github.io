using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class AdministerBusinessLayer
    {

        public void SetAdmin(int id, Boolean state)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();
            User user = (from m in db.Users
                         where m.UserID == id
                         select m).Single<User>();
            user.UAuthority = state;
            Operate o = new Operate();
            o.AdminID = int.Parse(s);
            o.UserID = id;
            o.OperateDate = DateTime.Now;
            if(state)
                o.OperateState = "授权";
            else
                o.OperateState = "撤销";
            db.Operates.Add(o);
            db.SaveChanges();
        }

        public void ChangeState(int id, Boolean state)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Apply apply = (from m in db.Applys
                            where m.ApplyID == id
                            select m).Single<Apply>();
            apply.IsDeal = true;

            string s = System.Web.HttpContext.Current.Session["UserID"].ToString();

            Diary d = new Diary();
            d.AdminID = int.Parse(s);
            d.ApplyID = id;
            d.DiaryDate = DateTime.Now;

            if (state)
            {
                User user = (from n in db.Users
                             where n.UserID == apply.UserID
                             select n).Single<User>();
                user.UAuthority = true;
               
                d.ApplyState = "批准";

                Operate o = new Operate();
                o.AdminID = d.AdminID;
                o.OperateDate = d.DiaryDate;
                o.UserID = id;
                o.OperateState = "授权";
                db.Operates.Add(o);
            }
            else
            {
                d.ApplyState = "驳回";
            }
            db.Diaries.Add(d);
            db.SaveChanges();
        }

        public List<User> GetUser(Boolean IsAdmin)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var users = from m in db.Users
                        select m;
            users = users.Where(s => s.UAuthority == IsAdmin);
            return users.ToList<User>();
        }

        public List<Apply> GetApply()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var applys = from m in db.Applys
                         select m;
            applys = applys.Where(s => s.IsDeal == false);
            return applys.ToList<Apply>();
        }

        public Apply SearchApply(int id)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var applys = from m in db.Applys
                         select m;
            applys = applys.Where(s => s.ApplyID == id);
            return applys.SingleOrDefault<Apply>();
        }

        public int GetPostingAmount(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            int num = 0;

            var contentions = from m in db.Contentions
                           select m;
            if (UID != 0)
            {
                contentions = contentions.Where(s => s.UserID == UID);
            }
            if (contentions.ToList<Contention>().Count == 0)
            {
                return 0;
            }

            num = contentions.ToList<Contention>().Count;
            return num;
        }

        public String GetAname(int AID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var admins = from m in db.Admins
                         select m;
            admins = admins.Where(s => s.AdminID == AID);
            return admins.SingleOrDefault<Admin>().Aname;
        }

        public List<Diary> GetDiaryList()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var diaries = from m in db.Diaries
                         select m;
            diaries = diaries.OrderByDescending(s => s.DiaryDate);
            return diaries.ToList<Diary>();
        }

        public List<Operate> GetOperateList()
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var operates = from m in db.Operates
                          select m;
            operates = operates.OrderByDescending(s => s.OperateDate);
            return operates.ToList<Operate>();
        }

    }
}