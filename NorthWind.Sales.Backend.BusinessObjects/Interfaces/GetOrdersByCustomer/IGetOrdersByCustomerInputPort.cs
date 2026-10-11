namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.GetOrdersByCustomer;

public interface IGetOrdersByCustomerInputPort
{
    Task Handle(string customerId);
}
