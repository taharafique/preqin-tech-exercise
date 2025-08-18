namespace InvestorsApi.Models
{
    public class Commitment
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public string AssetClass { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }
    }
}