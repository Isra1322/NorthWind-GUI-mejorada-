namespace NorthWind.Membership.Backend.AspNetIdentity.Options;

public class MembershipDBOptions
{
    public const string SectionKey = "MembershipDB";
    public string ConnectionString { get; set; } = string.Empty;
}
