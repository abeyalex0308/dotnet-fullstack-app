using Preqin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preqin.Application.DTOs
{
    public class CommitmentTotalByAssetClassDto
    {
        public AssetClass AssetClass { get; set; }
        public decimal TotalCommitments { get; set; }
        public string Asset { get; set; }
    }
}
