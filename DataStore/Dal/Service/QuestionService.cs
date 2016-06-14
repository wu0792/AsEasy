using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;


namespace DataStore.Dal
{
    public partial class QuestionService : DalBase, IQuestionService
    {
        public void Delete(List<long> idList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = GetIdTableFromList(idList);
                var cmd = new CommandDefinition("sp_Question_d#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_ID", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Delete(long id)
        {
            Delete(new List<long>() { id });
        }
        
        public void Insert(List<Question> questionList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();
                table.Columns.Add("QuestionID");
                table.Columns.Add("Title");
                table.Columns.Add("Remark");
                table.Columns.Add("Status");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");
      
                foreach (var entry in questionList)
                {
                    var row = table.NewRow();
                    row["QuestionID"] = entry.QuestionID;
                    row["Title"] = entry.Title;
                    row["Remark"] = entry.Remark;
                    row["Status"] = entry.Status;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_Question_i#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_Question#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }
 
        public void Insert(Question question)
        {
            Insert(new List<Question>() { question });
        }
 
        public void Update(List<Question> questionList)
        {
            using (var conn = GetConnection(false))
            {
                var args = new DynamicParameters();
                var table = new DataTable();                
                table.Columns.Add("QuestionID");
                table.Columns.Add("Title");
                table.Columns.Add("Remark");
                table.Columns.Add("Status");
                table.Columns.Add("DataCreated_Date");
                table.Columns.Add("DataCreated_By");
                table.Columns.Add("DataChanged_Date");
                table.Columns.Add("DataChanged_By");

                foreach (var entry in questionList)
                {
                    var row = table.NewRow();
                    row["QuestionID"] = entry.QuestionID;
                    row["Title"] = entry.Title;
                    row["Remark"] = entry.Remark;
                    row["Status"] = entry.Status;
                    row["DataCreated_Date"] = entry.DataCreated_Date;
                    row["DataCreated_By"] = entry.DataCreated_By;
                    row["DataChanged_Date"] = entry.DataChanged_Date;
                    row["DataChanged_By"] = entry.DataChanged_By;

                    table.Rows.Add(row);
                }

                var cmd = new CommandDefinition("sp_Question_u#0", commandType: CommandType.StoredProcedure, parameters: args);

                args.Add("@TVP_Question#0", dbType: DbType.Object, direction: ParameterDirection.Input, value: table);
                conn.ExecuteReader(cmd);
            }
        }

        public void Update(Question question)
        {
            Update(new List<Question>() { question });
        }

        public IEnumerable<Question> GetQuestions()
        {
            return GetQuestions("", null);
        }

        public IEnumerable<Question> GetQuestions(string where, DynamicParameters parameters)
        {
            using (var conn = GetConnection(false))
            {
                return conn.Query<Question>("select * from Question where " + (string.IsNullOrWhiteSpace(where) ? "1=1" : where), parameters);
            }
        }
        
        public Question GetSingleQuestion(string where, DynamicParameters parameters)
        {
            var questions = GetQuestions(where, parameters);
            return questions != null && questions.Any() ? questions.First() : null;
        }

        public Question GetQuestionByPk(long id)
        {
            return GetQuestions(" QuestionID = @QuestionID", new DynamicParameters(new { QuestionID = id })).FirstOrDefault();
        }

        public PageDataView<Question> GetList(int page, int pageSize = 10)
        {
            var criteria = new PageCriteria();

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "Question";
            criteria.PrimaryKey = "QuestionID";
            var r = GetPageData<Question>(criteria);
            return r;
        }
    }
}