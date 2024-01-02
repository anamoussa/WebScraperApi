namespace WebScraperApi.Models.AwardingResult;

public class GetAwardingResult
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CardBasicData")]
    public string tenderIdString { get; set; }
    public virtual CardBasicData? CardBasicData { get; set; }
    public List<AwardedSupplier> awardedSuppliers { get; set; } = new List<AwardedSupplier>();
    public List<OfferApplicant> offerApplicants { get; set; } = new List<OfferApplicant>();

}
