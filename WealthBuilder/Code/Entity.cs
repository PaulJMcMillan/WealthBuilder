using System.Linq;

namespace WealthBuilder.Code
{
    internal class Entity
    {
        internal static decimal GetLowestBalance()
        {
            using (var db = new WealthBuilderEntities1())
            {
                var entity = db.Entities.Where(x => x.Active == true && CurrentEntity.Id == x.Id).FirstOrDefault();
                if (entity == null) return 0;
                return (long)entity.LowestBalance;
            }
        }
    }
}