using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebScraperApi.Models
{
    public class GetDetailsForVisitor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CardBasicData")]
        public string tenderIdString { get; set; }
        public virtual CardBasicData? CardBasicData { get; set; }

        public string? CompetitionPurpose { get; set; }
        public string? CompetitionStatus { get; set; }
        public double ContractDuration { get; set; }
        public bool IsInsuranceRequired { get; set; }
        public string? OfferingMethod { get; set; }
        public bool IsPreliminaryGuaranteeRequired { get; set; }
        public string? PreliminaryGuaranteeAddress { get; set; }
        public double FinalGuarantee { get; set; }
        public string? AwardNumber { get; set; }
    }
}
