using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GoFit.Models
{
    public static class HtmlHelpers
    {

        /// <summary>
        /// generates AddLink helper which addds nested form
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">text to display on link</param>
        /// <param name="containerElement">where nested form is added</param>
        /// <param name="counterElement">counts index for controls naming</param>
        /// <param name="collectionProperty">name of the collection</param>
        /// <param name="nestedType">nested type</param>
        /// <returns>Html helper that adds nested form</returns>
        public static MvcHtmlString LinkToAddNestedForm<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string linkText, string containerElement, string counterElement, string cssClass = null) where TProperty : IEnumerable<object>
        {
            // a fake index to replace with a real index
            long ticks = DateTime.UtcNow.Ticks;
            // pull the name and type from the passed in expression
            string collectionProperty = ExpressionHelper.GetExpressionText(expression);
            var nestedObject = Activator.CreateInstance(typeof(TProperty).GetGenericArguments()[0]);

            // save the field prefix name so we can reset it when we're doing
            string oldPrefix = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;
            // if the prefix isn't empty, then prepare to append to it by appending another delimiter
            if (!string.IsNullOrEmpty(oldPrefix))
            {
                htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix += ".";
            }
            // append the collection name and our fake index to the prefix name before rendering
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix += string.Format("{0}[{1}]", collectionProperty, ticks);
            string partial = htmlHelper.EditorFor(x => nestedObject).ToHtmlString();

            // done rendering, reset prefix to old name
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix;

            // strip out the fake name injected in (our name was all in the prefix)
            partial = Regex.Replace(partial, @"[\._]?nestedObject", "");

            // encode the output for javascript since we're dumping it in a JS string
            partial = HttpUtility.JavaScriptStringEncode(partial);

            // create the link to render
            var js = string.Format("javascript:addNestedForm('{0}','{1}','{2}','{3}');return false;", containerElement, counterElement, ticks, partial);
            TagBuilder a = new TagBuilder("a");
            a.Attributes.Add("href", "javascript:void(0)");
            a.Attributes.Add("onclick", js);
            if (cssClass != null)
            {
                a.AddCssClass(cssClass);
            }
            a.InnerHtml = linkText;

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }

    }
}