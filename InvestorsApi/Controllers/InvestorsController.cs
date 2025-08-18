using InvestorsApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestorsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestorsController : ControllerBase
    {
        private readonly InvestorDbContext _investorContext;


        public InvestorsController(InvestorDbContext investorContext)
        {
            _investorContext = investorContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetInvestorsAsync()
        {
            var investors = await _investorContext.Investors
                    .Include(i => i.Commitments)
                    .Select(i => new InvestorDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Type = i.Type,
                        Country = i.Country,
                        DateAdded = i.DateAdded,
                        LastUpdated = i.LastUpdated,
                        TotalCommitments = i.Commitments.Sum(c => c.Amount)
                    })
                    .ToListAsync();

            var orderedInvestors = investors.OrderByDescending(i => i.TotalCommitments).ToList();

            return Ok(orderedInvestors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetInvestor(int id, [FromQuery] string assetClass = null)
        {
            var investor = await _investorContext.Investors
                .Include(i => i.Commitments)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (investor == null)
            {
                return NotFound();
            }

            var commitments = investor.Commitments.AsQueryable();
        
            if (!string.IsNullOrEmpty(assetClass))
            {
                commitments = commitments.Where(c => c.AssetClass == assetClass);
            }

            var commitmentsList = commitments
                .Select(c => new CommitmentDto
                {
                    Id = c.Id,
                    AssetClass = c.AssetClass,
                    Amount = c.Amount,
                    Currency = c.Currency
                })
                .ToList();

            var result = new InvestorDetailDto
            {
                Id = investor.Id,
                Name = investor.Name,
                Type = investor.Type,
                Country = investor.Country,
                DateAdded = investor.DateAdded,
                LastUpdated = investor.LastUpdated,
                Commitments = commitmentsList.OrderByDescending(c => c.Amount).ToList()
            };

            return Ok(result);
        }
    }
}