using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using System.Text;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper P_Html, PagingInfo P_PagingInfo, Func<int, string> P_PageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= P_PagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", P_PageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == P_PagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());

        }
    }
}