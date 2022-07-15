using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice3.Db.Entities
{
    public class Material : BaseEntity
    {
        public int ClassCode { get; set; }
        public int GroupCode { get; set; }
        public string Name { get; set; }
        public int MeasurementUnitId { get; set; }
        public MeasurementUnit? MeasurementUnit { get; set; }
    }
}
