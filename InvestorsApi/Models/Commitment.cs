namespace InvestorsApi.Models
{
    public class Commitment
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public string AssetClass { get; set; }

        public string Amount { get; set; }

        public string Currency { get; set; }
    }
}