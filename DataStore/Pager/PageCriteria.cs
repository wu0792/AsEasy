using System;

namespace DataStore.Pager
{
    public class PageCriteria
    {
        public PageCriteria()
        {
            Fields = "*";
            PrimaryKey = TableName + "ID";
            PageSize = 20;
            CurrentPage = 1;
            Sort = "DataChanged_Date desc";
            ClearCondition();
        }

        public string TableName { get; set; }
        public string Fields { get; set; }
        public string PrimaryKey { get; set; }
        public int PageSize { get; set; }
        private int _currentPage;
        public int CurrentPage
        {
            get { return this._currentPage; }
            set { this._currentPage = Math.Max(1, value); }
        }
        public string Sort { get; set; }
        private string _condition;
        public string Condition
        {
            get { return _condition; }
        }
        public int RecordCount { get; set; }


        public void ClearCondition()
        {
            _condition = string.Empty;
        }

        public void AppendCondition(string format, string value, bool onlyAppendIfNotEmpty = true)
        {
            var clearValue = ExcludeSensitiveChar(value);
            if (!string.IsNullOrWhiteSpace(clearValue) || !onlyAppendIfNotEmpty)
            {
                _condition += string.Format(format, clearValue);
            }
        }

        private string ExcludeSensitiveChar(string value)
        {
            return value;
        }
    }
}
