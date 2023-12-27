using Microsoft.EntityFrameworkCore;

namespace WebScraperApi.Models.Data;

public class ScrapDBContext : DbContext
{
    public ScrapDBContext(DbContextOptions<ScrapDBContext> options) : base(options)
    {

    }
}
