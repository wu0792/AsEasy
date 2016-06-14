 
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
        
        void Insert(List<OperLog> operLogList);
 
        void Insert(OperLog operLog);
 
        void Update(List<OperLog> operLogList);

        void Update(OperLog operLog);

        IEnumerable<OperLog> GetOperLogs();

        IEnumerable<OperLog> GetOperLogs(string where, DynamicParameters parameters);
        
        OperLog GetSingleOperLog(string where, DynamicParameters parameters);

        OperLog GetOperLogByPk(long id);

        PageDataView<OperLog> GetList(int page, int pageSize = 10);
    }
}