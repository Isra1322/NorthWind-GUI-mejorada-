namespace NorthWind.DomainLogs.Entities.Interfaces;

public interface IDomainLogger
{
    Task Log(DomainLog log);
}
