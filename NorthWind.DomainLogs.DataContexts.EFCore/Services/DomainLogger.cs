namespace NorthWind.DomainLogs.DataContexts.EFCore.Services;

internal class DomainLogger(NorthWindDomainLogsContext context) : IDomainLogger
{
    public async Task Log(DomainLog log)
    {
        await context.Logs.AddAsync(log);
        await context.SaveChangesAsync();
    }
}
