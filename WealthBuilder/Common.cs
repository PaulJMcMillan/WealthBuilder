namespace WealthBuilder
{
    class Common
    {
        internal static bool ConvertLongToBool(long? active)
        {
            if (active == null) return false;
            return active == 1;
        }

        internal static long? ConvertBoolToLong(bool? value)
        {
            if (value == null) return 0;
            return value==true ? 1 : 0;
        }

        internal static string GetFormText(string text)
        {
            return text + " (" + CurrentEntity.Name + ")";
        }
    }
}
