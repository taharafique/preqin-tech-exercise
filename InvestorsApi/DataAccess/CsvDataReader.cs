using CsvHelper;
using InvestorsApi.Models;
using InvestorsApi.Services;
using System.Globalization;

namespace InvestorsApi.DataAccess
{
    public class CsvDataReader : IInvestorRepository
    {
        private readonly List<Investor> _investors;

        public CsvDataReader()
        {
            _investors = ReadCsv();
        }

        public List<Investor> GetInvestors() => _investors;

        public Investor GetInvestor(int id) => _investors.FirstOrDefault(i => i.Id == id);

        private List<Investor> ReadCsv()
        {
            var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "data.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvRecord>();

            var investors = new Dictionary<string, Investor>();

            var nextInvestorId = 0;
            var nextCommitmentId = 0;

            foreach (var record in records)
            {
                if (!investors.ContainsKey(record.InvestorName))
                {
                    var investor = new Investor
                    {
                        Id = nextInvestorId++,
                        Name = record.InvestorName,
                        Type = record.InvestoryType,
                        Country = record.InvestorCountry,
                        DateAdded = DateTime.Parse(record.InvestorDateAdded),
                        LastUpdated = DateTime.Parse(record.InvestorLastUpdated),
                        Commitments = []
                    };

                    investors[record.InvestorName] = investor;
                }

                var commitment = new Commitment
                {
                    Id = nextCommitmentId++,
                    InvestorId = investors[record.InvestorName].Id,
                    Amount = record.CommitmentAmount,
                    AssetClass = record.CommitmentAssetClass,
                    Currency = record.CommitmentCurrency
                };

                investors[record.InvestorName].Commitments.Add(commitment);
            }

            return investors.Values.ToList();
        }
    }
}