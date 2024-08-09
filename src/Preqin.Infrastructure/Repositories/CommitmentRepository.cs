using Microsoft.EntityFrameworkCore;
using Preqin.Application.DTOs;
using Preqin.Application.Models;
using Preqin.Application.Repositories;
using Preqin.Core.Entities;
using Preqin.Core.Enums;
using Preqin.Infrastructure.Data;
using Preqin.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CommitmentRepository : BaseRepository<Commitment>, ICommitmentRepository
{
    public CommitmentRepository(PreqinDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<CommitmentDto>> GetCommitmentsByInvestorIdAsync(
    int investorId,
    int pageIndex,
    int pageSize,
    AssetClass? assetClass = null) // Make assetClass nullable and optional
    {
        var assetClassNames = Enum.GetValues(typeof(AssetClass))
                                  .Cast<AssetClass>()
                                  .ToDictionary(ac => (int)ac, ac => ac.ToString());

        var query = _dbSet
            .Where(c => c.InvestorId == investorId);

        if (assetClass.HasValue)
        {
            // If assetClass is provided, filter by asset class
            query = query.Where(c => c.AssetClass == assetClass.Value);
        }

     
        var projectedQuery = query.Select(c => new CommitmentDto
        {
            Id = c.Id,
            AssetClass = c.AssetClass,
            Asset = assetClassNames[(int)c.AssetClass],
            Amount = c.Amount,
            Currency = c.Currency,
            InvestorId = c.InvestorId
        });

        var totalCount = await projectedQuery.CountAsync();
        var items = await projectedQuery
            .OrderBy(c => c.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<CommitmentDto>(items, totalCount, pageIndex, pageSize);
    }

    

    public async Task<IEnumerable<CommitmentTotalByAssetClassDto>> GetTotalCommitmentsByAssetClassAsync(int investorId)
    {
        // Retrieve all commitments from the database
        var commitments = await _dbSet.ToListAsync();

        // Perform the grouping and summing in memory
        var totalCommitmentsByAssetClass = commitments
            .Where(c => c.InvestorId == investorId)
            .GroupBy(c => c.AssetClass)
            .Select(g => new CommitmentTotalByAssetClassDto
            {
                Asset = Enum.GetName(typeof(AssetClass), g.Key),
                AssetClass = g.Key,
                TotalCommitments = g.Sum(c => c.Amount)
            })
            .ToList();

        return totalCommitmentsByAssetClass;
    }

    
}
