using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataStore.Dal.Pager;

namespace AsEasy.Ext
{
    public static class PagingHelpersExt
    {
        public static MvcHtmlString PageLinks<T>(this HtmlHelper html, PageDataView<T> pageData,
            Func<int, string> pageUrl, int showMaxPageNum = 20)
        {
            const string tplPageNumLink = "<a href=\"{0}\">{1}</a>";
            const string tmplPageNumLinkCurrent = "<span class=\"current\">{0}</span>";
            var result = new StringBuilder();
            if (pageData.TotalPageCount > 0)
            {
                result.AppendFormat(@"第 {0} 页，共 {1} 页，共{2}条", (pageData.TotalPageCount < pageData.CurrentPage ? 0 : pageData.CurrentPage)
                                                            , pageData.TotalPageCount
                                                            , pageData.TotalNum);
                int showStartPageNum, showEndPageNum;
                if (pageData.CurrentPage <= 1)
                {
                    showStartPageNum = 1;
                    showEndPageNum = showMaxPageNum;
                    if (showEndPageNum > pageData.TotalPageCount)
                        showEndPageNum = pageData.TotalPageCount;
                }
                else if (pageData.CurrentPage >= pageData.TotalPageCount)
                {
                    showStartPageNum = pageData.TotalPageCount - showMaxPageNum + 1;
                    showEndPageNum = pageData.TotalPageCount;
                    if (showStartPageNum <= 0)
                        showStartPageNum = 1;
                }
                else
                {
                    int beforeCount = Convert.ToInt32(Math.Ceiling(showMaxPageNum * 1.0 / 2));
                    showStartPageNum = pageData.CurrentPage - beforeCount;
                    if (showStartPageNum <= 0)
                        showStartPageNum = 1;
                    showEndPageNum = showStartPageNum + showMaxPageNum - 1;
                    if (showEndPageNum > pageData.TotalPageCount)
                        showEndPageNum = pageData.TotalPageCount;
                    if (showStartPageNum > pageData.TotalPageCount)
                        showEndPageNum = pageData.TotalPageCount;
                }
                result.AppendFormat(tplPageNumLink, pageUrl(1), "首页");
                if (pageData.CurrentPage > 1)
                {
                    result.AppendFormat(tplPageNumLink, pageUrl(pageData.CurrentPage - 1), "上一页");
                }
                for (int i = showStartPageNum; i <= showEndPageNum; i++)
                {
                    if (i == pageData.CurrentPage)
                        result.AppendFormat(tmplPageNumLinkCurrent, i);
                    else
                        result.AppendFormat(tplPageNumLink, pageUrl(i), i);
                }
                if (pageData.CurrentPage < pageData.TotalPageCount)
                {
                    result.AppendFormat(tplPageNumLink, pageUrl(pageData.CurrentPage + 1), "下一页");
                }
                result.AppendFormat(tplPageNumLink, pageUrl(pageData.TotalPageCount), "末页");
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}