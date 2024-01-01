namespace WebScraperApi.Models;

public class AwardedSupplier
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("GetAwardingResult")]
    public int GetAwardingResultId { get; set; }
    public virtual GetAwardingResult? GetAwardingResult { get; set; }
    public string? Supplier_name { get; set; }
    public double Financial_offer { get; set; }
    public double Award_value { get; set; }
}
