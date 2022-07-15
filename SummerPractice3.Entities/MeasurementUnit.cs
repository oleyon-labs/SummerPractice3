using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice3.Db.Entities
{
    public class MeasurementUnit : BaseEntity
    {
        public string UnitName { get; set; }
        public List<Material> Materials { get; set; } = new();
    }
}
