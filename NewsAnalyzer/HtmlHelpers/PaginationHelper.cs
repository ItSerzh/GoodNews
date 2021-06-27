using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAnalyzer.Models;
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
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var str = $"<a class=\"btn btn-default\" href={pageUrl(i)}> {i}</a>";

                if (i == pageInfo.PageNumber)
                {
                    str = $"<a class=\"btn selected btn btn-primary\" href={pageUrl(i)}> {i}</a>";
                }
                sb.Append(str);
            }

            return new HtmlString(sb.ToString());
        }
    }
}
