using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.Utils.Html
{
    public static class HtmlBuild
    {
        public static string GetA(string text, string url,
            string activeCss = "", bool disabled = false)
        {
            if (disabled)
            {
                activeCss += " disabled";
            }
            return $"<a class=\"btn btn-default {activeCss}\" href={url}>{text}</a>";
        }

    }
}
