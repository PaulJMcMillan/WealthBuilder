using System.Linq;

namespace WealthBuilder.Repositories
{
    class FrequencyRepository
    {
        internal string GetName(int frequencyId)
        {
            using (var db = new WealthBuilderEntities1())
            {
                var frequency = db.Frequencies.Where(x => x.Id == frequencyId).FirstOrDefault();
                if (frequency == null) return string.Empty;
                return frequency.Name;
            }
        }
    }
}
