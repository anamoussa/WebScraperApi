namespace WebScraperApi.Models
{
    public class GetAwardingResult
    {
        public List<AwardedSupplier> awardedSuppliers { get; set; }=new List<AwardedSupplier>();
        public List<OfferApplicant> offerApplicants { get; set; } = new List<OfferApplicant>();
     
    }
}
