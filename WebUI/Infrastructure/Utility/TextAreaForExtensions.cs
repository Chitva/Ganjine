using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebUI.Infrastructure.Utility
{
    /// <summary>
    /// Represents support for HTML textarea controls, while working around the <see cref="TextAreaExtensions.TextAreaFor"/> bug,
    /// which prevents unobtrusive validation attributes from rendering correctly for complex expressions.
    /// </summary>
    /// <remarks>
    /// The bug is recognized by Microsoft as Issue #8576 on CodePlex (http://aspnet.codeplex.com/workitem/8576).
    /// </remarks>
    public static class TextAreaForExtensions
    {
        /// <summary>
        /// Returns an HTML textarea element with relevant validation attributes for each property in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
        /// <exception cref="ArgumentNullException">The expression parameter is null.</exception>
        public static MvcHtmlString TextAreaWithValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return TextAreaWithValidationFor(helper, expression, (IDictionary<string, object>)null);
        }

        /// <summary>
        /// Returns an HTML textarea element with relevant validation attributes for each property in the object that is represented by the specified expression using the specified HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
        /// <exception cref="ArgumentNullException">The expression parameter is null.</exception>
        public static MvcHtmlString TextAreaWithValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return TextAreaWithValidationFor(helper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Returns an HTML textarea element with relevant validation attributes for each property in the object that is represented by the specified expression using the specified HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
        /// <exception cref="ArgumentNullException">The expression parameter is null.</exception>
        public static MvcHtmlString TextAreaWithValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> validationAttributes = GetCorrectValidationAttributes<TModel, TProperty>(helper, expression);
            var mergedAttributes = MergeAttributes(htmlAttributes, validationAttributes);
            return TextAreaExtensions.TextAreaFor(helper, expression, mergedAttributes);
        }

        /// <summary>
        /// Returns an HTML textarea element with relevant validation attributes for each property in the object that is represented by the specified expression using the specified HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
        /// <exception cref="ArgumentNullException">The expression parameter is null.</exception>
        public static MvcHtmlString TextAreaWithValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            return TextAreaWithValidationFor(helper, expression, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Returns an HTML textarea element with relevant validation attributes for each property in the object that is represented by the specified expression using the specified HTML attributes.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="htmlAttributes">A dictionary that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML textarea element for each property in the object that is represented by the expression.</returns>
        /// <exception cref="ArgumentNullException">The expression parameter is null.</exception>
        public static MvcHtmlString TextAreaWithValidationFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            IDictionary<string, object> validationAttributes = GetCorrectValidationAttributes<TModel, TProperty>(helper, expression);
            var mergedAttributes = MergeAttributes(htmlAttributes, validationAttributes);
            return TextAreaExtensions.TextAreaFor(helper, expression, rows, columns, mergedAttributes);
        }

        #region Overloads for native MVC TextAreaFor methods

        [Obsolete("This method does not emit validation attributes for complex expressions. Use TextAreaWithValidationFor instead.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "This method is used to force developers to use TextAreaWithValidationFor")]
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            throw new NotSupportedException("Use TextAreaWithValidationFor instead.");
        }

        [Obsolete("This method does not emit validation attributes for complex expressions. Use TextAreaWithValidationFor instead.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlAttributes", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            throw new NotSupportedException("Use TextAreaWithValidationFor instead.");
        }

        [Obsolete("This method does not emit validation attributes for complex expressions. Use TextAreaWithValidationFor instead.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlAttributes", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            throw new NotSupportedException("Use TextAreaWithValidationFor instead.");
        }

        [Obsolete("This method does not emit validation attributes for complex expressions. Use TextAreaWithValidationFor instead.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "rows", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "columns", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlAttributes", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            throw new NotSupportedException("Use TextAreaWithValidationFor instead.");
        }

        [Obsolete("This method does not emit validation attributes for complex expressions. Use TextAreaWithValidationFor instead.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "rows", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "columns", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlAttributes", Justification = "This method is used to force developers to use TextAreaWithValidationFor.")]
        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            throw new NotSupportedException("Use TextAreaWithValidationFor instead.");
        }

        #endregion
        
        private static IDictionary<string, object> GetCorrectValidationAttributes<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            // The bug occurs because TextAreaExtensions.TextAreaHelper() does not pass the ModelMetadata to HtmlHelper.GetUnobtrusiveValidationAttributes().
            // The sequence of actions is as follows:
            // TextAreaFor obtains model metadata by calling ModelMetadata.FromLambdaExpression()
            // TextAreaFor invokes TextAreaHelper, passing the model metadata to it
            // TextAreaHelper invokes HtmlHelper.GetUnobtrusiveValidationAttributes(name)
            // HtmlHelper.GetUnobtrusiveValidationAttributes(name) calls HtmlHelper.GetUnobtrusiveValidationAttributes(name, modelMedatada: null)
            // HtmlHelper.GetUnobtrusiveValidationAttributes(name, modelMedatada: null) calls HtmlHelper.ClientValidationRuleFactory(name, modelMedatada: null)
            // HtmlHelper.ClientValidationRuleFactory(name, modelMedatada: null) creates ModelMetadata by calling ModelMetadata.FromStringExpression(name, this.ViewData)
            // ModelMetadata.FromStringExpression does not support parsing of complex expressions (it expects just a property name).
            // As a result, when expression is more complex than a simple property access on the top-level model object, validation attributes are not emitted.
            // To work around the bug, we must invoke GetUnobtrusiveValidationAttributes ourselves, passing correct modelMetadata obtained from ModelMetadata.FromLambdaExpression().
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var name = ExpressionHelper.GetExpressionText(expression);
            IDictionary<string, object> validationAttributes = helper.GetUnobtrusiveValidationAttributes(name, modelMetadata);
            return validationAttributes;
        }

        private static IDictionary<string, object> MergeAttributes(IDictionary<string, object> htmlAttributes, IDictionary<string, object> validationAttributes)
        {
            if (htmlAttributes == null)
            {
                return validationAttributes;
            }

            if (validationAttributes == null)
            {
                return htmlAttributes;
            }

            // htmlAttributes take precedence, since they are provided by the user.
            // When an attribute is present in both dictionaries, its initial value (taken from validationAttributes) will be overwritten with value from htmlAttributes.
            var result = new Dictionary<string, object>(validationAttributes);
            foreach (var kvp in htmlAttributes)
            {
                result[kvp.Key] = kvp.Value;
            }

            return result;
        }
    }
}