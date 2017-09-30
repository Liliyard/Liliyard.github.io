using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialWebSite.BusinessLayer
{
    public class PersonalBusinessLayer
    {
        public User GetUserInfo(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var users = from m in db.Users
                        select m;
            User u = new User();

            if(UID != 0)
            {
                users = users.Where(s => s.UserID == UID);
            }

            u = users.FirstOrDefault<User>();
            return u;
        }

        public String GetUPhone(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var users = from m in db.Users
                        select m;
            User u = new User();

            if (UID != 0)
            {
                users = users.Where(s => s.UserID == UID);
            }

            u = users.FirstOrDefault<User>();
            return u.UPhone;
        }

        public String GetUPassword(int UID)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            var users = from m in db.Users
                        select m;
            User u = new User();

            if (UID != 0)
            {
                users = users.Where(s => s.UserID == UID);
            }

            u = users.FirstOrDefault<User>();
            return u.UPassword;
        }

        public void SavePassword(int UID, String newPassword)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            User tmp = new User();
            tmp = (from m in db.Users
                   where m.UserID == UID
                   select m).Single<User>();
            tmp.UPassword = newPassword;
            db.SaveChanges();
        }

    }
}