using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AsEasy.Ext
{
    public static class AuthenticationContextExt
    {
        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        /// <param name="filterContext"></param>
        public static void RedirectToLogin(this AuthenticationContext filterContext)
        {
            var Url = new UrlHelper(filterContext.RequestContext);
            var url = Url.Action("Login", "Account", new { area = "" });
            filterContext.Result = new RedirectResult(url);
        }
    }
}