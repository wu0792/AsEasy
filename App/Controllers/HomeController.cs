using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsEasy.Attrs;
using AsEasy.Common;
using Dapper;
using DataStore.Dal;
using DataStore.Entity;

namespace AsEasy.Controllers
{
    public class HomeController : BaseController
    {
        [AuthLogin]
        public ActionResult Index()
        {
            var roleService = new RoleService();
            var pageIndex = GetPageIndexFromQuery();
            var pageSize = GetPageSizeFromQuery();

            var roles = roleService.GetList(pageIndex, pageSize);
            return View(roles);
        }

        [AuthPermission(Permission.HomeViewMessage)]
        [AuthLogin]
        public ActionResult HomeViewMessage()
        {
            return Content("HomeViewMessage");
        }
    }
}
