using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsEasy.Common;
using UtilTool;

namespace AsEasy.Controllers
{
    public class BaseController : Controller
    {
        #region Action跳转
        /// <summary>
        /// 清空登录信息，跳转到登录页面
        /// </summary>
        public void RedirectToLogin()
        {
            Session.Remove(Const.SessionKey.LoginUser);
            RedirectToAction("Login", "Account");
        }

        #endregion

        #region 获取登录信息
        /// <summary>
        /// 是否已经登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return Session[Const.SessionKey.LoginUser] is ExUser;
            }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public ExUser ExUser
        {
            get
            {
                return Session[Const.SessionKey.LoginUser] as ExUser;
            }
        }
        #endregion

        #region 获取查询参数信息
        protected int GetPageSizeFromQuery()
        {
            return GetIntValueFromRequest("pageSize", 20);
        }

        protected int GetPageIndexFromQuery()
        {
            return GetIntValueFromRequest("pageIdx", 1);
        }

        protected string GetStringValueFromRequest(string key, string defaultVal = "")
        {
            var reqVal = (Request[key] ?? "").Trim();
            if (string.IsNullOrWhiteSpace(reqVal))
            {
                return defaultVal;
            }

            return reqVal;
        }

        protected int GetIntValueFromRequest(string key, int defaultVal = 0)
        {
            var reqVal = (Request[key] ?? "").Trim();
            if (string.IsNullOrWhiteSpace(reqVal))
            {
                return defaultVal;
            }

            return DataTypeConvert.ToInt(reqVal, defaultVal);
        }
        #endregion
    }
}