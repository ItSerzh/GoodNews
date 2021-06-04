using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsAnalyzer.Helpers
{
    public static class HtmlCleanup
    {
        public static string RemoveInlineStyles(string inVal)
        {
            string  pattern = @"style\s *=\"".*?\""";
            var retVal = Regex.Replace(inVal, pattern, "");
            return retVal;
        }

        public static HtmlNode RemoveElement(HtmlNode html, string[] tagNames, string[] css, string[] ids)
        {
            var retVal = html.Clone();
            retVal.RemoveAll();
            if (html != null)
            {
                foreach (var tag in html.ChildNodes)
                {
                    if (css.Length != 0)
                    {
                        var tagClass = tag.Attributes["class"];
                        if (tagClass != null && css.Contains(tagClass.Value, StringComparer.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    if (ids.Length != 0)
                    {
                        var tagId = tag.Attributes["id"];
                        if (tagId != null && ids.Contains(tagId.Value, StringComparer.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    if (tagNames.Length != 0)
                    {
                        var tagName = tag.Name;
                        if (!string.IsNullOrEmpty(tagName) && tagNames.Contains(tagName, StringComparer.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    retVal.AppendChild(tag);
                }
            }
            return retVal;
        }

        public static HtmlNode RemoveElementsByXpath(HtmlNode html, string[] xPaths)
        {
            if (html != null)
            {
                foreach (var xPath in xPaths)
                {
                    var nodes = html.SelectNodes(xPath);
                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            node.Remove();
                        }
                    }
                }
            }
            return html;
        }
        public static HtmlNode ReplaceBackgroundImageWithImg(HtmlNode html, string xPath)
        {
            var image = html.SelectSingleNode(xPath);
            var styles = image.GetAttributeValue("style", "").Split('\'');
            if (styles.Length > 1)
            {
                var newNode = HtmlNode.CreateNode($"<img src=\"{styles[1]}\">");
                image.ParentNode.ReplaceChild(newNode, image);
            }
            return html;
        }
    }
}
