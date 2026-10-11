namespace NorthWind.DomainLogs.DataContexts.EFCore.DataContexts;

internal class NorthWindDomainLogsContext(
    IOptions<DomainLogsOptions> options) : DbContext
{
    public DbSet<DomainLog> Logs => Set<DomainLog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(options.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainLog>(b =>
        {
            b.Property<int>("Id");
            b.HasKey("Id");
            b.Property(l => l.DateTime).IsRequired();
            b.Property(l => l.User).IsRequired().HasMaxLength(50);
            b.Property(l => l.Description).IsRequired().HasMaxLength(250);
        });
    }
}
