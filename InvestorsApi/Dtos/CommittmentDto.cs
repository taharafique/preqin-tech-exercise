public class CommitmentDto
{
    public int Id { get; set; }

    public string AssetClass { get; set; } = string.Empty;

    public double Amount { get; set; }
    
    public string Currency { get; set; } = string.Empty;
}