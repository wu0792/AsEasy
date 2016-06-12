using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;

namespace DataStore.Dal
{
    public partial interface IRoleService
    {
        void Delete(List<long> idList);

        void Delete(long id);
        
        void Insert(List<Role> RoleList);
 
        void Insert(Role Role);
 
        void Update(List<Role> RoleList);

        void Update(Role Role);

        IEnumerable<Role> GetRoles();

        IEnumerable<Role> GetRoles(string where, DynamicParameters parameters);
        
        Role GetSingleRole(string where, DynamicParameters parameters);

        Role GetRoleByPk(long id);

        PageDataView<Role> GetList(string name, int page, int pageSize = 10);
    }
}