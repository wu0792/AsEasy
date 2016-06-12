using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Dal.Pager
{
    public class PageDataView<T>
    {
        public PageDataView()
        {
            this.Items = new List<T>();
        }

        public int TotalNum { get; set; }

        public IList<T> Items { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPageCount { get; set; }
    }
}
