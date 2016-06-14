using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;


namespace DataStore.Dal
{
    public partial class OperLogService : DalBase, IOperLogService
    {
        public void Delete(List<long> idList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = GetIdTableFromList(idList);
                var cmd = new CommandDefinition("sp_OperLog_d#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_ID", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Delete(long id)
        {
            Delete(new List<long>() { id });
        }
        
        public void Insert(List<OperLog> operLogList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();
                table.Columns.Add("LogID");
                table.Columns.Add("DataType");
                table.Columns.Add("OperType");
                table.Columns.Add("EntityID");
                table.Columns.Add("Content");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");
      
                foreach (var entry in operLogList)
                {
                    var row = table.NewRow();
                    row["LogID"] = entry.LogID;
                    row["DataType"] = entry.DataType;
                    row["OperType"] = entry.OperType;
                    row["EntityID"] = entry.EntityID;
                    row["Content"] = entry.Content;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_OperLog_i#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_OperLog#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }
 
        public void Insert(OperLog operLog)
        {
            Insert(new List<OperLog>() { operLog });
        }
 
        public void Update(List<OperLog> operLogList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();                
                table.Columns.Add("LogID");
                table.Columns.Add("DataType");
                table.Columns.Add("OperType");
                table.Columns.Add("EntityID");
                table.Columns.Add("Content");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");

                foreach (var entry in operLogList)
                {
                    var row = table.NewRow();
                    row["LogID"] = entry.LogID;
                    row["DataType"] = entry.DataType;
                    row["OperType"] = entry.OperType;
                    row["EntityID"] = entry.EntityID;
                    row["Content"] = entry.Content;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_OperLog_u#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_OperLog#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Update(OperLog operLog)
        {
            Update(new List<OperLog>() { operLog });
        }

        public IEnumerable<OperLog> GetOperLogs()
        {
            return GetOperLogs("", null);
        }

        public IEnumerable<OperLog> GetOperLogs(string where, DynamicParameters parameters)
        {
            using (var conn = GetConnection(false))
            {
                return conn.Query<OperLog>("select * from OperLog where " + (string.IsNullOrWhiteSpace(where) ? "1=1" : where), parameters);
            }
        }
        
        public OperLog GetSingleOperLog(string where, DynamicParameters parameters)
        {
            var operLogs = GetOperLogs(where, parameters);
            return operLogs != null && operLogs.Any() ? operLogs.First() : null;
        }

        public OperLog GetOperLogByPk(long id)
        {
            return GetOperLogs(" LogID = @LogID", new DynamicParameters(new { LogID = id })).FirstOrDefault();
        }

        public PageDataView<OperLog> GetList(int page, int pageSize = 10)
        {
            var criteria = new PageCriteria();

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "OperLog";
            criteria.PrimaryKey = "LogID";
            var r = GetPageData<OperLog>(criteria);
            return r;
        }
    }
}