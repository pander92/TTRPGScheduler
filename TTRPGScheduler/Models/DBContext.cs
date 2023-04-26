using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TTRPGScheduler.Models;

namespace TTRPGScheduler.Models
{
    public class DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBContext(DbContextOptions<DBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("TTRPGScheduler");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Player> Player { get; set; } = null!;
        public DbSet<PlayerAttendance> PlayerAttendance { get; set; } = null!;
        public DbSet<ProposedSession> ProposedSession { get; set; } = null!;
    }
}

