using Microsoft.EntityFrameworkCore.Design;

namespace NorthWind.Membership.Backend.AspNetIdentity.DataContexts;

public class NorthWindMembershipContextFactory : IDesignTimeDbContextFactory<NorthWindMembershipContext>
{
    public NorthWindMembershipContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NorthWindMembershipContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NorthWindUsersDB;Integrated Security=true;TrustServerCertificate=True;");
        return new NorthWindMembershipContext(optionsBuilder.Options);
    }
}
