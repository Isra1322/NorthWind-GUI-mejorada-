namespace NorthWind.Sales.Backend.UseCases.Resources;

public static class CreateOrderMessages
{
    public const string CustomerIdNotFoundError = "El identificador de cliente proporcionado no existe.";
    public const string CustomerWithBalanceErrorTemplate = "El cliente {0} tiene un adeudo pendiente de {1}.";
    public const string ProductIdNotFoundErrorTemplate = "El producto {0} no existe.";
    public const string UnitsInStockLessThanQuantityErrorTemplate = "Cantidad {0} de {1} no suficiente para producto {2}.";
    public const string CreatedOrderLogTemplate = "Orden {0} creada para el cliente {1}.";
    public const string OrderCreationCancelledTemplate = "Creación de orden {0} cancelada.";
}
