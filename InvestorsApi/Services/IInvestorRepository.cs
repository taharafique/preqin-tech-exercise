using InvestorsApi.Models;

namespace InvestorsApi.Services
{
    public interface IInvestorRepository
    {
        List<Investor> GetInvestors();

        Investor GetInvestor(int id);
    }
}