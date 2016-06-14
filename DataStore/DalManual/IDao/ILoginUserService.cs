using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;

namespace DataStore.Dal
{
    public partial interface ILoginUserService
    {
        PageDataView<LoginUser> GetList(string name, string status, int page, int pageSize = 10);
    }
}