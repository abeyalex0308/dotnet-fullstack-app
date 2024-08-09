using Preqin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preqin.Core.Entities
{
    public class Commitment
    {
        public int Id { get; set; }
        public AssetClass AssetClass { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int InvestorId { get; set; }
        public virtual Investor Investor { get; set; }
    }
}
