using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Preqin.Application.DTOs;
using Preqin.Application.Models;
using Preqin.Application.Repositories;
using Preqin.Core.Enums;

namespace Preqin.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api/investors")]
    public class InvestorsController : ControllerBase
    {
        private readonly IInvestorsRepository _investorService;
        private readonly ICommitmentRepository _commitmentService;

        public InvestorsController(IInvestorsRepository investorService, ICommitmentRepository commitmentService)
        {
            _investorService = investorService;
            _commitmentService = commitmentService;
        }

        // GET /api/investors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestorWithTotalCommitmentsDto>>> GetInvestors([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var investors = await _investorService.GetInvestorsWithTotalCommitmentsAsync(pageIndex, pageSize);
            //throw new NotImplementedException();
            return Ok(investors);
        }
        
        // GET /api/investors/{investorId}/commitments
        [HttpGet("{investorId}/commitments")]
        public async Task<ActionResult<PaginatedList<CommitmentDto>>> GetInvestorCommitments(
            int investorId,
            [FromQuery] AssetClass? assetClass,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            // Call the combined service method with the optional assetClass parameter
            var commitments = await _commitmentService.GetCommitmentsByInvestorIdAsync(investorId, pageIndex, pageSize, assetClass);
            return Ok(commitments);
        }


        // GET /api/investors/{investorId}/commitments/totalByAssetClass
        [HttpGet("{investorId}/commitments/totalByAssetClass")]
        public async Task<ActionResult<IEnumerable<CommitmentTotalByAssetClassDto>>> GetTotalCommitmentsByAssetClass(int investorId)
        {
            var totals = await _commitmentService.GetTotalCommitmentsByAssetClassAsync(investorId);
            return Ok(totals);
        }
    }
}
