namespace WebScraperApi.Models
{
    public class ExecutionLocation
    { 
        [Key]
        public int Id { get; set; }
        public bool InSideKingdom { get; set; }
        public string? City { get; set; }
        public List<Region>? Regions { get; set; }=new List<Region>();

        [ForeignKey("GetRelationsDetail")]
        public int GetRelationsDetailId { get; set; }
        public virtual GetRelationsDetail? GetRelationsDetail { get; set; }

    }
}
