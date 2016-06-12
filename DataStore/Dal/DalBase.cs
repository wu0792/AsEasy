using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Pager;

namespace DataStore.Dal
{
    public class DalBase
    {
        protected static readonly string ConnString = "data source=WU0792-PC\\WU0792;initial catalog=AsEasy;persist security info=True;user id=sa;password=w;";

        public SqlConnection GetConnection(bool needOpen = true)
        {
            var connection = new SqlConnection(ConnString);
            if (needOpen)
            {
                connection.Open();
            }
            return connection;
        }

        protected DataTable GetIdTableFromList(List<long> idList)
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(long));
            foreach (var id in idList)
            {
                var row = table.NewRow();
                row["ID"] = id;
                table.Rows.Add(row);
            }
            return table;
        }

        protected static PageDataView<T> GetPageData<T>(PageCriteria criteria)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                var p = new DynamicParameters();
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var pageData = new PageDataView<T>();
                pageData.Items = conn.Query<T>("sp_GetPageData", p, commandType: CommandType.StoredProcedure).ToList();
                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
        }
    }
}
