using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UrsaCoder.MvcJsI18n
{
    public static class JsI18nHelper
    {
        /// <summary>
        /// Html Helper for include js resource files to page.
        /// </summary>
        /// <param name="helper">parameter for setting the extension to the HtmlHelper objects.</param>
        /// <param name="resourceName">parameter for specify the resource name.</param>
        /// <returns></returns>
        public static HtmlString IncludeJavascriptResources(this HtmlHelper helper, string resourceName, string path = null, string locale = null)
        {
            if (locale == null)
            {
                locale = System.Globalization.CultureInfo.CurrentCulture.Name;

                if (JsI18nCore.UseShortLocale && locale.Length > 2)
                    locale = locale.Substring(0, 2);
            }

            if (locale == JsI18nCore.DefaultLocale || locale == null)
                return new MvcHtmlString("<script src='" + JsI18nCore.DefaultTargetPath + resourceName + ".js' type='text/javascript'></script>");
            else
                return new MvcHtmlString("<script src='" + JsI18nCore.DefaultTargetPath + resourceName + "." + locale + ".js' type='text/javascript'></script>");
        }
    }
}
