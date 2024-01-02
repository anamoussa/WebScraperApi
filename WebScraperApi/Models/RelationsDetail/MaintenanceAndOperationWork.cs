﻿namespace WebScraperApi.Models.RelationsDetail
{
    public class MaintenanceAndOperationWork
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("GetRelationsDetail")]
        public int GetRelationsDetailId { get; set; }
        public virtual GetRelationsDetail? GetRelationsDetail { get; set; }
    }
}
