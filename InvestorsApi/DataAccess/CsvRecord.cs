using CsvHelper.Configuration.Attributes;

namespace InvestorsApi.DataAccess
{
    public class CsvRecord
    {
        [Name("Investor Name")]
        public string InvestorName { get; set; } = string.Empty;

        [Name("Investory Type")]
        public string InvestoryType { get; set; } = string.Empty;

        [Name("Investor Country")]
        public string InvestorCountry { get; set; } = string.Empty;

        [Name("Investor Date Added")]
        public string InvestorDateAdded { get; set; } = string.Empty;

        [Name("Investor Last Updated")]
        public string InvestorLastUpdated { get; set; } = string.Empty;

        [Name("Commitment Asset Class")]
        public string CommitmentAssetClass { get; set; } = string.Empty;

        [Name("Commitment Amount")]
        public string CommitmentAmount { get; set; } = string.Empty;

        [Name("Commitment Currency")]
        public string CommitmentCurrency { get; set; } = string.Empty;
    }
}