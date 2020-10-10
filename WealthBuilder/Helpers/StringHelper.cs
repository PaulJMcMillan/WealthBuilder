namespace WealthBuilder.Helpers
{
    class StringHelper
    {
        internal static decimal ConvertToDecimalWithEmptyString(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            string t = text.Replace("$", string.Empty).Replace(",", string.Empty);

            if(decimal.TryParse(t, out decimal convertedVal))
            {
                return convertedVal;
            }

            return 0;
        }

        internal static string StripDollarSignAndCommas(string text)
        {
            return text.Replace("$", string.Empty).Replace(",", string.Empty);
        }

    }
}
