using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsEasy.Common;
using Dapper;
using DataStore.Dal;

namespace AsEasy.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            string username = "";
            string password = "";
            if (username == "admin" && password == "admin")
            {
                Session["AdminUser"] = username;
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ViewBag.Error = "用户名或密码不正确！";
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginSubmit(FormCollection form)
        {
            var loginName = form["loginName"];
            var pwd = form["pwd"];
            var encryPwd = UtilTool.MD5.Encrypt(string.Format("{0}#{1}", loginName, pwd), Const.EncryKey.EncrySeed);
            var loginUser = DalFactory.LoginUserService.GetSingleLoginUser(" LoginName=@LoginName and Password=@Password ", new DynamicParameters(new { LoginName = loginName, Password = encryPwd }));
            if (loginUser != null)
            {
                Session[Const.SessionKey.LoginUser] = new ExUser()
                {
                    User = loginUser
                };

                return RedirectToAction("Index", "Home");
            }

            return Content("wrong.");
        }

        public ActionResult Logout()
        {
            Session["AdminUser"] = null;
            return RedirectToAction("Login");
        }
    }
}