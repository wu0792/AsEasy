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
        void Delete(List<long> idList);

        void Delete(long id);
        
        void Insert(List<LoginUser> LoginUserList);
 
        void Insert(LoginUser LoginUser);
 
        void Update(List<LoginUser> LoginUserList);

        void Update(LoginUser LoginUser);

        IEnumerable<LoginUser> GetLoginUsers();

        IEnumerable<LoginUser> GetLoginUsers(string where, DynamicParameters parameters);
        
        LoginUser GetSingleLoginUser(string where, DynamicParameters parameters);

        LoginUser GetLoginUserByPk(long id);

        PageDataView<LoginUser> GetList(string name, int page, int pageSize = 10);
    }
}