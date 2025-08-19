namespace InvestorsApi.Models
{
    public class Investor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Country { get; set; }

        public string DateAdded { get; set; }

        public string LastUpdated { get; set; }

        public List<Commitment> Commitments { get; set; }
    }
}