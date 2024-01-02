namespace WebScraperApi.Models.RelationsDetail;

public class GetRelationsDetail
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CardBasicData")]
    public string tenderIdString { get; set; } = null!;
    public virtual CardBasicData? CardBasicData { get; set; }

    public List<ExecutionLocation>? ExecutionLocations { get; set; } = new();
    public string? Details { get; set; }
    public List<CompetitionActivity>? CompetitionActivities { get; set; } = new();
    public string? SupplyItemsCompetition { get; set; }
    public List<ConstructionWork> ConstructionWorks { get; set; }=new();
    public List<MaintenanceAndOperationWork> MaintenanceAndOperationWorks { get; set; } = new();

}
