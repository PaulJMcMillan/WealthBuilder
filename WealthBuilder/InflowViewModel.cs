using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthBuilder
{
    public class InflowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime InflowDate { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }
        public int EntityId { get; set; }
    }
}
