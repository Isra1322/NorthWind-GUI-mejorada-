namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.CustomerSearch;

public interface ICustomerSearchOutputPort
{
    PagedResult<CustomerSearchDto>? Result { get; }
    Task Handle(PagedResult<CustomerSearchDto> result);
}
