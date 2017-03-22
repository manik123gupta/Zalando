using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalando.Helper
{
    public static class JsonExtensions
    {
        public static string GetString(this JToken token, string tag, string defaultValue = "")
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
                return value.ToString();
            else
                return defaultValue;
        }

        public static int GetInt(this JToken token, string tag, int defaultValue = 0)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                int result;
                return !int.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static long GetLong(this JToken token, string tag, long defaultValue = 0)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                long result;
                return !long.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static int? GetIntNullable(this JToken token, string tag, int? defaultValue = null)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                int result;
                return !int.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static double GetDouble(this JToken token, string tag, double defaultValue = 0)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                double result;
                return !double.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static double? GetDoubleNullable(this JToken token, string tag, double? defaultValue = null)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                double result;
                return !double.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static bool? GetBoolean(this JToken token, string tag, bool? defaultValue = false)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                bool result;

                if (value.ToString().Trim().ToLower() == "y" || value.ToString().Trim().ToLower() == "yes")
                    return true;
                else if (value.ToString().Trim().ToLower() == "n" || value.ToString().Trim().ToLower() == "no")
                    return false;
                else
                    return !bool.TryParse(value.ToString(), out result) ? defaultValue : result;
            }
            else
                return defaultValue;
        }

        public static JToken GetObject(this JToken token, string tag, JToken defaultValue = null)
        {
            if (token == null)
                return new JObject();

            var result = token.SelectToken(tag) as JToken;
            return result != null ? result : defaultValue;
        }

        public static DateTime? GetDateTime(this JToken token, string tag, DateTime? defaultValue = null)
        {
            if (token == null) return defaultValue;

            var value = token.SelectToken(tag);
            if (value != null)
            {
                DateTime result = DateTime.ParseExact(value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                return result;
            }
            else
                return defaultValue;
        }
    }
}
