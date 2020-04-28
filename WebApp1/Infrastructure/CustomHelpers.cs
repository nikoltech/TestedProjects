namespace WebApp1.Infrastructure
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Web.Mvc;

    public static class CustomHelpers
    {
        public static MvcHtmlString DisplayMessage(this IHtmlHelper html, string msg)
        {
            //string encodedMessage = html.Encode(msg);
            string result = String.Format("Сообщение: <p>{0}</p>", msg);
            return new MvcHtmlString(result);
        }
    }
}
