using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;


namespace DataStore.Dal
{
    public partial class RoleService : DalBase, IRoleService
    {
        public void Delete(List<long> idList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = GetIdTableFromList(idList);
                var cmd = new CommandDefinition("sp_Role_d#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_ID", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Delete(long id)
        {
            Delete(new List<long>() { id });
        }
        
        public void Insert(List<Role> roleList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();
                table.Columns.Add("RoleID");
                table.Columns.Add("Name");
                table.Columns.Add("Remark");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");
      
                foreach (var entry in roleList)
                {
                    var row = table.NewRow();
                    row["RoleID"] = entry.RoleID;
                    row["Name"] = entry.Name;
                    row["Remark"] = entry.Remark;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_Role_i#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_Role#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }
 
        public void Insert(Role role)
        {
            Insert(new List<Role>() { role });
        }
 
        public void Update(List<Role> roleList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();                
                table.Columns.Add("RoleID");
                table.Columns.Add("Name");
                table.Columns.Add("Remark");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");

                foreach (var entry in roleList)
                {
                    var row = table.NewRow();
                    row["RoleID"] = entry.RoleID;
                    row["Name"] = entry.Name;
                    row["Remark"] = entry.Remark;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_Role_u#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_Role#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Update(Role role)
        {
            Update(new List<Role>() { role });
        }

        public IEnumerable<Role> GetRoles()
        {
            return GetRoles("", null);
        }

        public IEnumerable<Role> GetRoles(string where, DynamicParameters parameters)
        {
            using (var conn = GetConnection(false))
            {
                return conn.Query<Role>("select * from Role where " + (string.IsNullOrWhiteSpace(where) ? "1=1" : where), parameters);
            }
        }
        
        public Role GetSingleRole(string where, DynamicParameters parameters)
        {
            var roles = GetRoles(where, parameters);
            return roles != null && roles.Any() ? roles.First() : null;
        }

        public Role GetRoleByPk(long id)
        {
            return GetRoles(" RoleID = @RoleID", new DynamicParameters(new { RoleID = id })).FirstOrDefault();
        }

        public PageDataView<Role> GetList(string name, int page, int pageSize = 10)
        {
            var criteria = new PageCriteria();
            criteria.AppendCondition(" Name like '{0}%' ", name);

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "Role";
            criteria.PrimaryKey = "RoleID";
            var r = GetPageData<Role>(criteria);
            return r;
        }
    }
}