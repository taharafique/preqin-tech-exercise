using InvestorsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestorsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestorsController : ControllerBase
    {
        private readonly IInvestorRepository _repository;

        public InvestorsController(IInvestorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetInvestors()
        {
            var investors = _repository.GetInvestors();

            return Ok(investors);
        }

        [HttpGet("{id}")]
        public ActionResult GetInvestor(int id)
        {
            var investors = _repository.GetInvestor(id);

            return Ok(investors);
        }
    }
}