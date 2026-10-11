namespace NorthWind.DomainLogs.Entities.ValueObjects;

public class DomainLog
{
    public DomainLog(DateTime dateTime, string user, string description)
    {
        DateTime = dateTime;
        User = user;
        Description = description;
    }

    protected DomainLog()
    {
        User = default!;
        Description = default!;
    }

    public DateTime DateTime { get; private set; }
    public string User { get; private set; }
    public string Description { get; private set; }
}
