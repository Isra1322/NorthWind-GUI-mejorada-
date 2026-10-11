namespace NorthWind.Sales.Backend.BusinessObjects.Events;

public class SpecialOrderCreatedEvent(int orderId, int productsCount) : IDomainEvent
{
    public int OrderId => orderId;
    public int ProductsCount => productsCount;
}
