using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WeaponDoc
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

    }

    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Gets the IP address of the client sending the request. This method will return the originating
        /// IP if specified by a proxy but makes no guarantee that this is the client's true IP address.
        /// Since these headers can be spoofed, you are encouraged to perform additional validation if
        /// you are using the IP in a sensitive context.
        /// </summary>
        ///// <param name="httpRequest">
        /// The incoming request to get the client's IP address from.
        /// This is typically from HttpContext.Current.Request or similar.
        /// </param>
        public static string ClientIP(this HttpRequest httpRequest)
        {
            var ip = httpRequest.Headers["X-Forwarded-For"] ??
                        httpRequest.Headers["CF-Connecting-IP"] ??
                        httpRequest.ServerVariables["REMOTE_HOST"];

            if (ip.Contains(","))
            {
                ip = ip.Split(',').First().Trim();
            }

            return ip;
        }

            
    }
    
}