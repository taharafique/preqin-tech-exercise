using CsvHelper;
using InvestorsApi.Context;
using InvestorsApi.Models;
using System.Globalization;

namespace InvestorsApi.DataAccess
{
    public static class CsvDataReader
    {
        public static void ReadCsv(InvestorDbContext dbContext)
        {
            var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "data.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvRecord>();

            var investors = new Dictionary<string, Investor>();

            foreach (var record in records)
            {
                if (!investors.ContainsKey(record.InvestorName))
                {
                    var investor = new Investor
                    {
                        Name = record.InvestorName,
                        Type = record.InvestoryType,
                        Country = record.InvestorCountry,
                        DateAdded = record.InvestorDateAdded,
                        LastUpdated = record.InvestorLastUpdated,
                        Commitments = []
                    };

                    investors[record.InvestorName] = investor;
                }

                var commitment = new Commitment
                {
                    InvestorId = investors[record.InvestorName].Id,
                    Amount = double.Parse(record.CommitmentAmount),
                    AssetClass = record.CommitmentAssetClass,
                    Currency = record.CommitmentCurrency
                };

                investors[record.InvestorName].Commitments.Add(commitment);
            }

            dbContext.Investors.AddRange(investors.Values);
            dbContext.SaveChanges();
        }
    }
}