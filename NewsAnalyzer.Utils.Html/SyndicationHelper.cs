using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace NewsAnalyzer.Utils.Html
{
    public static class SyndicationHelper
    {
        public static string GetSyndicationItemSummary(SyndicationItem syndicationItem)
        {
            var retVal = syndicationItem.Summary?.Text;
            if (string.IsNullOrEmpty(retVal))
            {
                retVal = ((TextSyndicationContent)syndicationItem.Content).Text;
            }
            return HtmlCleanup.RemoveInlineStyles(retVal);
        }
    }
}
