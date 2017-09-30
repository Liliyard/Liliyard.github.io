using FinancialWebSite.Data_Access_Layer;
using FinancialWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace FinancialWebSite.BusinessLayer
{
    public class AuthenticationBusinessLayer
    {
        public void SavePassword(String UName, String newPassword)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            User tmp = new User();
            var user = from m in db.Users
                       select m;

            user = user.Where(s => s.UName.Equals(UName));
            tmp = user.SingleOrDefault<User>();
            tmp.UPassword = newPassword;

            db.SaveChanges();
        }


        public User SaveUser(User u)
        {
            FinancialERPDAL finanDal = new FinancialERPDAL();
            finanDal.Users.Add(u);
            finanDal.SaveChanges();
            return u;
        }

        // 查询用户
        public User SearchUser(string searchString)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            User u = new User();
            var users = from m in db.Users
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UName.Equals(searchString));
            }
            u = users.FirstOrDefault<User>();
            return u;
        }

        public Admin SearchAdmin(string searchString)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            Admin a = new Admin();
            var admins = from m in db.Admins
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(s => s.Aname.Equals(searchString));
            }
            a = admins.FirstOrDefault<Admin>();
            return a;
        }

        public String GetUPhone(string searchString)
        {
            FinancialERPDAL db = new FinancialERPDAL();
            User u = new User();
            var users = from m in db.Users
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UName.Equals(searchString));
            }
            u = users.FirstOrDefault<User>();
            return u.UPhone;
        }

        public UserStatus GetUserValidity(UserDetails u, String uIdentity)
        {
            if (uIdentity == "User")
            {
                User user = SearchUser(u.UserName);
                if (user != null && user.UName == u.UserName && user.UPassword == u.Password)
                    return UserStatus.AuthentucatedUser;
                else
                    return UserStatus.NonAuthenticatedUser;
            }
            else if (uIdentity == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else
                return UserStatus.NonAuthenticatedUser;
        }
    }
}