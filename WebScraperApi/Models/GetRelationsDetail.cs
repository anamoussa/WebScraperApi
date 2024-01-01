namespace WebScraperApi.Models;

public class GetRelationsDetail
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CardBasicData")]
    public string tenderIdString { get; set; }
    public virtual CardBasicData? CardBasicData { get; set; }

    public List<ExecutionLocation>? ExecutionLocations { get; set; }
    public string? Details { get; set; }
    public List<CompetitionActivity>? CompetitionActivities { get; set; } = new();
    public string? SupplyItemsCompetition { get; set; }
    public string? ConstructionWorks { get; set; }
    public string? MaintenanceAndOperationWorks { get; set; }

}
