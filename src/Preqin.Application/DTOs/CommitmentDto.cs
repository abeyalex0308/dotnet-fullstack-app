using Preqin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preqin.Application.DTOs
{
    public class CommitmentDto
    {
        public int Id { get; set; }
        public AssetClass AssetClass { get; set; }
        public string Asset { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int InvestorId { get; set; }


    }
}
