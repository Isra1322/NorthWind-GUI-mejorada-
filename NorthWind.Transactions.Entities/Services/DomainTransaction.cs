namespace NorthWind.Transactions.Entities.Services;

internal class DomainTransaction : IDomainTransaction, IDisposable
{
    private TransactionScope? transactionScope;

    public void BeginTransaction()
    {
#pragma warning disable CA1416 // Solo se admite en 'windows'
        TransactionManager.ImplicitDistributedTransactions = true;
#pragma warning restore CA1416
        transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    public void CommitTransaction()
    {
        transactionScope?.Complete();
        Dispose();
    }

    public void RollbackTransaction()
    {
        Dispose();
    }

    public void Dispose()
    {
        transactionScope?.Dispose();
        transactionScope = null;
    }
}
