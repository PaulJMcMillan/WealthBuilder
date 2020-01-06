//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System.Drawing;
using System;

namespace WealthBuilder
{
    public class Constants
    {
        private static readonly string FontName = "Microsoft Sans Serif";
        private static readonly float FontSize = 8.25f;
        public static readonly Font NormalFont = new Font(FontName, FontSize, FontStyle.Regular);
        public static readonly Font BoldFont = new Font(FontName, FontSize, FontStyle.Bold);
        public static readonly int DateColumnWidth = 85;
        public static readonly Font CellFont = NormalFont;
        public static readonly int FrequencyColumnWidth = 118;
        public static readonly int CurrencyColumnWidth = 77;
        public static readonly int NameColumnWidth = 270;
        public static readonly int DescriptionColumnWidth = 405;
        public static readonly int ClearedColumnWidth = 60;
        public static readonly int CheckNumberColumnWidth = 60;
        public static readonly int ReconciledColumnWidth = 80;
        public static readonly Color NormalFontColor = Color.FromArgb(0, 0, 0);
        public static readonly Color GridBackColor = Color.WhiteSmoke;
        public static readonly Color GridHeaderBackColor = SystemColors.HotTrack;
        public static readonly Color GridOddRowColor = GridBackColor;
        public static readonly Color GridEvenRowColor = SystemColors.ControlLightLight;
        public static readonly Color ButtonBackColor = SystemColors.HotTrack;
        public static readonly Color GridHeaderForeColor = Color.White;
        public static readonly int RowHeaderColumnWidth = 18;
        public static readonly string ApplicationUserFriendlyName = "Wealth Builder";
        public static readonly string WealthBuilderPolicyId = "564c70d3-28d9-4766-b76c-8697596c8796";
        public static readonly string PennyPincherPolicyId = "a5e7e5ba-67b8-4ef8-8a83-3d20b5eae4c1";
        public static readonly string DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\McMillan Financial Solutions\" + ApplicationUserFriendlyName + @"\";
        public static string CopyrightMessage = "Copyright \u00A9 2017-" + DateTime.Today.Year.ToString() + " McMillan Financial Solutions, LLC.  All rights reserved.";
        public static readonly string UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string ReconciliationReportFileName = UserFolder + @"\ReconciliationReport.txt";


        // Available at: https://app.keygen.sh/settings
        public const string KeyGenAccountId = "06019573-3891-4c24-a8f1-1e1515e49804";
        public const int x1 = 17;
        public const int x2 = 140;
        public const int x3 = 263;
        public const int y1 = 50;
        public const int y2 = 97;
        public const int y3 = 146;
        public const int y4 = 193;
        public const int y5 = 240;
        public const int y6 = 287;
        public const int y7 = 334;
    }
}