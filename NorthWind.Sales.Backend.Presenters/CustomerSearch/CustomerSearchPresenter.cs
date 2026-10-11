namespace NorthWind.Sales.Backend.Presenters.CustomerSearch;

internal class CustomerSearchPresenter : ICustomerSearchOutputPort
{
    public PagedResult<CustomerSearchDto>? Result { get; private set; }

    public Task Handle(PagedResult<CustomerSearchDto> result)
    {
        Result = result;
        return Task.CompletedTask;
    }
}
