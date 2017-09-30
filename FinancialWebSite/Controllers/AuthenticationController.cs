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
using System.Web.Security;
using WebApplication1.Models;

namespace FinancialWebSite.Controllers
{    
    public class AuthenticationController : Controller
    {
        public ActionResult ShowAlert(string msg)
        {
            var script = string.Format("alert('{0}');", msg);
            return JavaScript(script);
        }

        [HttpPost]
        public ActionResult ChangePassword(String btnSubmit, String NewPassword, User u)
        {
            AuthenticationBusinessLayer abl = new AuthenticationBusinessLayer();
            switch (btnSubmit)
            {
                case "提交":
                    if(abl.GetUPhone(u.UName).Equals(u.UPhone))
                    {
                        abl.SavePassword(u.UName, NewPassword);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return Content("手机号验证失败！");
                    }
                case "取消":
                    return RedirectToAction("Login");
            }
            return new EmptyResult();
        }


        [HeaderFilter]
        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }

        [HttpPost]
        [HeaderFilter]
        public ActionResult doRegister(User u)
        {
            AuthenticationBusinessLayer abl = new AuthenticationBusinessLayer();

            if (abl.SearchUser(u.UName) == null)
            {
                abl.SaveUser(u);
                UserDetails ud = new UserDetails();
                ud.UserName = u.UName;
                ud.Password = u.UPassword;
                
                return DoLogin(ud, "User");
            }                
            else
            {
                return Content("该用户名已存在！");
            }                           
        }

        public ActionResult submitRegister(String BtnRegister, User u)
        {
            switch(BtnRegister)
            {
                case "Register":
                    u.UAuthority = false;
                    return doRegister(u);
                case "Cancel": return RedirectToAction("Login");
            }
            return new EmptyResult();
        }

        [HttpPost]
        [HeaderFilter]
        public ActionResult DoLogin(UserDetails u, String uIdentity)
        {
            if (ModelState.IsValid)
            {
                AuthenticationBusinessLayer bal = new AuthenticationBusinessLayer();
                UserStatus status = bal.GetUserValidity(u, uIdentity);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthentucatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    Admin admin = new Admin();
                    admin = bal.SearchAdmin(u.UserName);
                    Session["UserName"] = admin.Aname;
                    Session["UserID"] = admin.AdminID;
                    return RedirectToAction("Manager", "Administer");
                }                   
                else
                {
                    User user = new User();
                    user = bal.SearchUser(u.UserName);
                    Session["UserName"] = user.UName;
                    Session["UserID"] = user.UserID;
                    Session["UserAuthority"] = user.UAuthority;
                    return RedirectToAction("Index", "Index");
                }
                    
                
                
                
                //New Code End
            }
            else
            {
                return View("Login");
            }
        }

        [HttpPost]
        [HeaderFilter]
        public ActionResult submitLogin(UserDetails u, String btnLogin , String uIdentity)
        {
            switch (btnLogin)
            {
                case "Login": return DoLogin(u, uIdentity);
                case "Register": return RedirectToAction("Register");
            }
            return new EmptyResult();
        }

        [HeaderFilter]
        public ActionResult Register()
        {
            return View("Register");
        }

        // GET: Authentication
        [HeaderFilter]
        public ActionResult Login()
        {

            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}