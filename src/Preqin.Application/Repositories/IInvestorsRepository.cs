using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Preqin.Application.DTOs;
using Preqin.Application.Models;
using Preqin.Core.Entities;

namespace Preqin.Application.Repositories
{
    public interface IInvestorsRepository : IBaseRepository<Investor>
    {
        Task<PaginatedList<InvestorWithTotalCommitmentsDto>> GetInvestorsWithTotalCommitmentsAsync(int pageIndex, int pageSize);
    }
}
