using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;


namespace DataStore.Dal
{
    public partial class LoginUserService : DalBase, ILoginUserService
    {
        public void Delete(List<long> idList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = GetIdTableFromList(idList);
                var cmd = new CommandDefinition("sp_LoginUser_d#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_ID", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Delete(long id)
        {
            Delete(new List<long>() { id });
        }
        
        public void Insert(List<LoginUser> loginUserList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();
                table.Columns.Add("UserID");
                table.Columns.Add("LoginName");
                table.Columns.Add("DispalyName");
                table.Columns.Add("Status");
                table.Columns.Add("Password");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");
      
                foreach (var entry in loginUserList)
                {
                    var row = table.NewRow();
                    row["UserID"] = entry.UserID;
                    row["LoginName"] = entry.LoginName;
                    row["DispalyName"] = entry.DispalyName;
                    row["Status"] = entry.Status;
                    row["Password"] = entry.Password;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_LoginUser_i#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_LoginUser#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }
 
        public void Insert(LoginUser loginUser)
        {
            Insert(new List<LoginUser>() { loginUser });
        }
 
        public void Update(List<LoginUser> loginUserList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();                
                table.Columns.Add("UserID");
                table.Columns.Add("LoginName");
                table.Columns.Add("DispalyName");
                table.Columns.Add("Status");
                table.Columns.Add("Password");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");

                foreach (var entry in loginUserList)
                {
                    var row = table.NewRow();
                    row["UserID"] = entry.UserID;
                    row["LoginName"] = entry.LoginName;
                    row["DispalyName"] = entry.DispalyName;
                    row["Status"] = entry.Status;
                    row["Password"] = entry.Password;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_LoginUser_u#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_LoginUser#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Update(LoginUser loginUser)
        {
            Update(new List<LoginUser>() { loginUser });
        }

        public IEnumerable<LoginUser> GetLoginUsers()
        {
            return GetLoginUsers("", null);
        }

        public IEnumerable<LoginUser> GetLoginUsers(string where, DynamicParameters parameters)
        {
            using (var conn = GetConnection(false))
            {
                return conn.Query<LoginUser>("select * from LoginUser where " + (string.IsNullOrWhiteSpace(where) ? "1=1" : where), parameters);
            }
        }
        
        public LoginUser GetSingleLoginUser(string where, DynamicParameters parameters)
        {
            var loginUsers = GetLoginUsers(where, parameters);
            return loginUsers != null && loginUsers.Any() ? loginUsers.First() : null;
        }

        public LoginUser GetLoginUserByPk(long id)
        {
            return GetLoginUsers(" UserID = @UserID", new DynamicParameters(new { UserID = id })).FirstOrDefault();
        }

        public PageDataView<LoginUser> GetList(int page, int pageSize = 10)
        {
            var criteria = new PageCriteria();

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