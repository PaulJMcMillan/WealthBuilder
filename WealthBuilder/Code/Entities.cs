using System.Collections.Generic;
using System.Linq;

namespace WealthBuilder.Code
{
    public static class Entities
    {
        public static List<WealthBuilder.Entity> GetActive()
        {
            using (var db = new WBEntities())
            {
                var rs = db.Entities.Where(x => x.Active == true).OrderBy(x => x.Name).ToList();
                var entities = new List<WealthBuilder.Entity>();

                foreach (var r in rs)
                {
                    var entity = new WealthBuilder.Entity();
                    entity.Id = r.Id;
                    entity.Name = r.Name;
                    entities.Add(entity);
                }

                return entities;
            }
        }
    }
}
