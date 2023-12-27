using Microsoft.EntityFrameworkCore;

namespace WebScraperApi.Models.Data;

public class ScrapDBContext : DbContext
{
    public DbSet<AwardedSupplier>AwardedSuppliers  { get; set; }
    public DbSet<CardBasicData>CardBasicDatas { get; set; }
    public DbSet<DetailsForVisitor>DetailsForVisitors  { get; set; }
    public DbSet<GetAwardingResult> GetAwardingResults { get; set; }
    public DbSet<GetRelationsDetail> GetRelationsDetails { get; set; }
    public DbSet<GetTenderDate>GetTenderDates { get; set; }
    public DbSet<OfferApplicant>offerApplicants { get; set; }

    public ScrapDBContext(DbContextOptions<ScrapDBContext> options) : base(options)
    {
    }


}
