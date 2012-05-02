using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IvtLibrary.Helpers
{
    public static class DataTransformators
    {
        public static MvcHtmlString GetAbbreviatedName(this HtmlHelper htmlHelper, string firstName, string middleName, string lastName)
        {
            StringBuilder result = new StringBuilder();

            result.Append(lastName);

            if(!string.IsNullOrEmpty(firstName))
            {
                result.Append(" " + firstName[0] + ".");

                if(!string.IsNullOrEmpty(middleName))
                {
                    result.Append(middleName[0] + ".");
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}