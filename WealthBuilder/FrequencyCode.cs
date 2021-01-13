using System.Linq;

namespace WealthBuilder
{
    public static class FrequencyCode
    {
        public const string Once = "Once";
        public const string Daily = "Daily";
        public const string Weekly = "Weekly";
        public const string BiWeekly = "BiWeekly";  //Every 2 Weeks
        public const string Monthly = "Monthly";
        public const string Quarterly = "Quarterly";
        public const string SemiAnnually = "SemiAnnually";
        public const string Annual = "Annual";

        public static string GetById(int Id)
        {
            using (var db = new WealthBuilderEntities1())
            {
                var r = db.Frequencies.Where(x => x.Id == Id).FirstOrDefault();
                if (r == null || r.Code == null) return string.Empty;
                return r.Code;
            }
        }
    }
}
