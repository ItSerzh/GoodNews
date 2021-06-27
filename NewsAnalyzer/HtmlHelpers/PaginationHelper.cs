using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAnalyzer.Models;
using NewsAnalyzer.Utils.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.HtmlHelpers
{
    public static class PaginationHelper
    {
        public static HtmlString CreatePagination(this IHtmlHelper html,
           PageInfo pageInfo,
           Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            sb.Append(HtmlBuild.GetA("First", pageUrl(1), disabled: pageInfo.PageNumber == 1));
            sb.Append(HtmlBuild.GetA("Prev", pageUrl(pageInfo.PageNumber - 1), disabled: pageInfo.PageNumber == 1));
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                if (i >= pageInfo.PageNumber - 3 && i <= pageInfo.PageNumber + 3)
                {
                    var str = HtmlBuild.GetA(i.ToString(), pageUrl(i));

                    if (i == pageInfo.PageNumber)
                    {
                        str = HtmlBuild.GetA(i.ToString(), pageUrl(i), "btn-primary"); ;
                    }
                    sb.Append(str);
                }
            }
            sb.Append(HtmlBuild.GetA("Next", pageUrl(pageInfo.PageNumber + 1), disabled: pageInfo.PageNumber == pageInfo.TotalPages));
            sb.Append(HtmlBuild.GetA("Last", pageUrl(pageInfo.TotalPages), disabled: pageInfo.PageNumber == pageInfo.TotalPages));
            return new HtmlString(sb.ToString());
        }
    }
}
