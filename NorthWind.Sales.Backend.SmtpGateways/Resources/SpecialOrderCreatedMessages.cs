namespace NorthWind.Sales.Backend.SmtpGateways.Resources;

public static class SpecialOrderCreatedMessages
{
    public const string EmailSubject = "Notificación de orden especial creada";
    public const string EmailBodyTemplate = "Se ha creado la orden {0} con {1} productos.";
}
