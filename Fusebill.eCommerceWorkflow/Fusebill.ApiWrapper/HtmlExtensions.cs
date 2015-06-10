using System.Linq.Expressions;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using System.Web.Mvc;
using System;

namespace Fusebill.ApiWrapper
{
    public static class HtmlExtensions
    {
        public static System.Web.HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented };
            return new System.Web.HtmlString(JsonConvert.SerializeObject(model, settings)); 
        }

        public static System.Web.IHtmlString AutofocusTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool enableAutofocus = true)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (enableAutofocus)
            {
                attributes["autofocus"] = "autofocus";
            }
            return htmlHelper.TextBoxFor(expression, attributes);
        }
    }
}

