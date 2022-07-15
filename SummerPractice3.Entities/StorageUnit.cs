using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice3.Db.Entities
{
    public class StorageUnit : BaseEntity
    {
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public ulong BalanceAccount { get; set; }
        public int AccompanyingDocumentCode { get; set; }
        public int AccompanyingDocumentNumber { get; set; }
        public double Count { get; set; }
        public double UnitCost { get; set; }
        public int MaterialSupplierId { get; set; }
        public MaterialSupplier? MaterialSupplier { get; set; }
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
    }
}
