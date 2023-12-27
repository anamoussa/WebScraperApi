using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebScraperApi.Models
{
    public class GetRelationsDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CardBasicData")]
        public string tenderIdString { get; set; }
        public virtual CardBasicData? CardBasicData { get; set; }

        public string? ExecutionLocation { get; set; }
        public string? Details { get; set; }
        public string? CompetitionActivity { get; set; }
        public string? SupplyItemsCompetition { get; set; }
        public string? ConstructionWorks { get; set; }
        public string? MaintenanceAndOperationWorks { get; set; }

    }
}
