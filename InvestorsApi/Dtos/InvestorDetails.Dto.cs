public class InvestorDetailDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string DateAdded { get; set; }

    public string LastUpdated { get; set; }

    public List<CommitmentDto> Commitments { get; set; } = [];
}