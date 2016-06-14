using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;

namespace DataStore.Dal
{
    public partial class LoginUserService : ILoginUserService
    {
        public PageDataView<LoginUser> GetList(string name, string status, int page, int pageSize = 10)
        {
            var criteria = new PageCriteria();
            criteria.AppendCondition(" and Name like '%{0}%'", name);
            criteria.AppendCondition(" and Status = {0}", status);

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "LoginUser";
            criteria.PrimaryKey = "UserID";
            var r = GetPageData<LoginUser>(criteria);
            return r;
        }
    }
}