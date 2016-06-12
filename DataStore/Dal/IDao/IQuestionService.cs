using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataStore.Dal.Pager;
using DataStore.Entity;
using DataStore.Pager;

namespace DataStore.Dal
{
    public partial interface IQuestionService
    {
        void Delete(List<long> idList);

        void Delete(long id);
        
        void Insert(List<Question> QuestionList);
 
        void Insert(Question Question);
 
        void Update(List<Question> QuestionList);

        void Update(Question Question);

        IEnumerable<Question> GetQuestions();

        IEnumerable<Question> GetQuestions(string where, DynamicParameters parameters);
        
        Question GetSingleQuestion(string where, DynamicParameters parameters);

        Question GetQuestionByPk(long id);

        PageDataView<Question> GetList(string name, int page, int pageSize = 10);
    }
}