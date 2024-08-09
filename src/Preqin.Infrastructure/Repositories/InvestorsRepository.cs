using Microsoft.EntityFrameworkCore;
using Preqin.Application.DTOs;
using Preqin.Application.Models;
using Preqin.Application.Repositories;
using Preqin.Core.Entities;
using Preqin.Infrastructure.Data;
using Preqin.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

public class InvestorsRepository : BaseRepository<Investor>, IInvestorsRepository
{
    public InvestorsRepository(PreqinDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<InvestorWithTotalCommitmentsDto>> GetInvestorsWithTotalCommitmentsAsync(int pageIndex, int pageSize)
    {

        var investorsQuery = _dbSet
            .Select(investor => new
            {
                investor.Id,
                investor.Name,
                investor.InvestorType,
                investor.Country,
                investor.DateAdded,
                investor.LastUpdated,
                Commitments = investor.Commitments
            });

        var totalCount = await investorsQuery.CountAsync();

       
        var investors = await investorsQuery
            .OrderBy(i => i.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(i => new
            {
                i.Id,
                i.Name,
                i.InvestorType,
                i.Country,
                DateAdded = i.DateAdded.ToString(),
                LastUpdated = i.LastUpdated.ToString(),
                i.Commitments
            })
            .ToListAsync();

       
        var items = investors.Select(i => new InvestorWithTotalCommitmentsDto
        {
            Id = i.Id,
            Name = i.Name,
            InvestorType = i.InvestorType,
            Country = i.Country,
            DateAdded = ParseDateTime(i.DateAdded),
            LastUpdated = ParseDateTime(i.LastUpdated),
            TotalCommitments = i.Commitments.Sum(c => c.Amount) 
        }).ToList();

    
        return new PaginatedList<InvestorWithTotalCommitmentsDto>(items, totalCount, pageIndex, pageSize);
    }

    private DateTime ParseDateTime(string dateString)
    {
        DateTime parsedDate;
        if (DateTime.TryParseExact(dateString.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            return parsedDate;
        }
        else
        {
            throw new FormatException($"Unable to parse '{dateString}' as a valid DateTime.");
        }
    }


}
