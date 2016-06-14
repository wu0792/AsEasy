 
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
        
        void Insert(List<Question> questionList);
 
        void Insert(Question question);
 
        void Update(List<Question> questionList);

        void Update(Question question);

        IEnumerable<Question> GetQuestions();

        IEnumerable<Question> GetQuestions(string where, DynamicParameters parameters);
        
        Question GetSingleQuestion(string where, DynamicParameters parameters);

        Question GetQuestionByPk(long id);

        PageDataView<Question> GetList(int page, int pageSize = 10);
    }
}