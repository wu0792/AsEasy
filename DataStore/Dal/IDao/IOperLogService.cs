using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;

namespace DataStore.Dal
{
    public partial interface IOperLogService
    {
        void Delete(List<long> idList);

        void Delete(long id);
        
        void Insert(List<OperLog> OperLogList);
 
        void Insert(OperLog OperLog);
 
        void Update(List<OperLog> OperLogList);

        void Update(OperLog OperLog);

        IEnumerable<OperLog> GetOperLogs();

        IEnumerable<OperLog> GetOperLogs(string where, DynamicParameters parameters);
        
        OperLog GetSingleOperLog(string where, DynamicParameters parameters);

        OperLog GetOperLogByPk(long id);

        PageDataView<OperLog> GetList(string name, int page, int pageSize = 10);
    }
}