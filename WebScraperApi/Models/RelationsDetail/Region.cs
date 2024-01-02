namespace WebScraperApi.Models.RelationsDetail
{
    public class Region
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("ExecutionLocation")]
        public int ExecutionLocationId { get; set; }
        public virtual ExecutionLocation? ExecutionLocation { get; set; }
        public string? Name { get; set; }

    }
}
