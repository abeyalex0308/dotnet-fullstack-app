using Preqin.Application.DTOs;
using Preqin.Application.Models;
using Preqin.Core.Entities;
using Preqin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preqin.Application.Repositories
{
    public interface ICommitmentRepository : IBaseRepository<Commitment>
    {
        Task<PaginatedList<CommitmentDto>> GetCommitmentsByInvestorIdAsync(int investorId, int pageIndex, int pageSize, AssetClass? assetClass);
        Task<IEnumerable<CommitmentTotalByAssetClassDto>> GetTotalCommitmentsByAssetClassAsync(int investorId);
    }
}
