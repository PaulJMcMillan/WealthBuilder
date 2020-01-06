using System.Linq;

namespace WealthBuilder.Code
{
    internal class Entity
    {
        internal static double GetLowestBalance()
        {
            using (var db = new WBEntities())
            {
                var entity = db.Entities.Where(x => x.Active == true && CurrentEntity.Id == x.Id).FirstOrDefault();
                if (entity == null || entity.LowestBalance==null) return 0;
                return (long)entity.LowestBalance;
            }
        }
    }
}