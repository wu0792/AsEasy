using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsEasy.Common;
using Dapper;
using DataStore.Dal;
using Newtonsoft.Json.Converters;
using UtilTool;

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
            var checkCode = form["checkCode"];
            var storedCheckCode = UtilTool.DataTypeConvert.ToStr(Session[Const.SessionKey.LoginCheckCode], "");
            if (checkCode != storedCheckCode)
            {
                var msg = new MessageModel()
                {
                    IsSuccess = false,
                    Message = "wrong checkCode"
                };

                return Json(msg);
            }

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

            var msg2 = new MessageModel()
            {
                IsSuccess = false,
                Message = "wrong name/pwd"
            };

            return Json(msg2);
        }

        public ActionResult Logout()
        {
            Session["AdminUser"] = null;
            return RedirectToAction("Login");
        }

        public void CreateCheckCode()
        {
            var checkCode = "1234";
            Session[Const.SessionKey.LoginCheckCode] = checkCode;

            using (System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22))
            using (Graphics g = Graphics.FromImage(image))
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //生成随机生成器
                var random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                var font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);
                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}