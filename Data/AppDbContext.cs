using Microsoft.EntityFrameworkCore;
using PivotTest.Models;

namespace PivotTest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ResponseReportPivot> ResponseReportPivot { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
