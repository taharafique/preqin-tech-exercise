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

        // [HttpGet("{id}")]
        // public ActionResult GetInvestor(int id)
        // {
        //     var investors = _repository.GetInvestor(id);

        //     return Ok(investors);
        // }
    }
}