using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice3.Db.Entities
{
    public class MaterialSupplier : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public ulong INN { get; set; }
        public string BusinessAddress { get; set; }
        public ulong BankAccount { get; set; }
        public int BankId { get; set; }
        public Bank? Bank { get; set; }
    }
}
