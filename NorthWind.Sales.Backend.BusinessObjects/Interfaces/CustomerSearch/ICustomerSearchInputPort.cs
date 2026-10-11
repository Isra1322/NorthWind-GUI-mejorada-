namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.CustomerSearch;

public interface ICustomerSearchInputPort
{
    Task Handle(CustomerSearchCriteriaDto criteria);
}
