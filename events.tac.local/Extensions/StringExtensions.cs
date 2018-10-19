using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace events.tac.local.Extensions
{
    public static class StringExtensions
    {
        public static string FindItemLookupValue(this string itemStringId, string fieldName)
        {
            var refItem = Sitecore.Context.Database.GetItem(itemStringId);
            if (refItem == null)
            {
                return string.Empty;
            }
            var value = refItem[fieldName];
            return value;
        }

        public static string ThrowIfNull(this string obj, string paramName)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ArgumentNullException($"String parameter {paramName} is null or empty");
            }

            return obj;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string EmptyIfNull(this string value)
        {
            return value ?? string.Empty;
        }

        public static string SafeString(this string value)
        {
            return value ?? "";
        }


    }
}