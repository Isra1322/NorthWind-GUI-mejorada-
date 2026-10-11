namespace NorthWind.DomainLogs.DataContexts.EFCore.Options;

public class DomainLogsOptions
{
    public const string SectionKey = "DomainLogs";
    public string ConnectionString { get; set; } = default!;
}
