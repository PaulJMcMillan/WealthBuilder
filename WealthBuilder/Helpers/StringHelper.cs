namespace WealthBuilder.Helpers
{
    class StringHelper
    {
        internal static decimal ConvertToDecimalWithEmptyString(string text)
        {
            //assumes that text is a valid currency with $ and commas in it.
            if (string.IsNullOrWhiteSpace(text)) return 0;
            string t = text.Replace("$", string.Empty).Replace(",", string.Empty);
            return decimal.Parse(t);
        }

        internal static string StripDollarSignAndCommas(string text)
        {
            return text.Replace("$", string.Empty).Replace(",", string.Empty);
        }

    }
}
