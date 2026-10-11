namespace NorthWind.Sales.Entities.ValueObjects;

public class Endpoints
{
    public const string CreateOrder = $"/{nameof(CreateOrder)}";
    public const string CustomerSearch = "/customer/search";
    public const string ProductAvailable = "/product/available";
    public const string OrderById = "/order/{id:int}";
    public const string OrdersByCustomer = "/order/customer/{customerId}";
}
