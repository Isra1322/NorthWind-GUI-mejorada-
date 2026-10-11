namespace NorthWind.Membership.Backend.AspNetIdentity.DataContexts;

public class NorthWindMembershipContext(DbContextOptions<NorthWindMembershipContext> options)
    : IdentityDbContext<NorthWindUser>(options)
{
}
