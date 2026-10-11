namespace Microsoft.AspNetCore.Builder;

public static class OrderInvoiceController
{
    public static WebApplication UseOrderInvoiceController(
        this WebApplication app)
    {
        app.MapGet(Endpoints.OrderById, GetOrderById);
        app.MapGet(Endpoints.OrdersByCustomer, GetOrdersByCustomer);
        return app;
    }

    public static async Task<IResult> GetOrderById(
        int id,
        IGetOrderInvoiceInputPort inputPort,
        IGetOrderInvoiceOutputPort presenter)
    {
        await inputPort.Handle(id);
        return presenter.Invoice is not null
            ? Results.Ok(presenter.Invoice)
            : Results.NotFound(new { Message = $"Order with ID {id} was not found." });
    }

    public static async Task<IResult> GetOrdersByCustomer(
        string customerId,
        IGetOrdersByCustomerInputPort inputPort,
        IGetOrdersByCustomerOutputPort presenter)
    {
        await inputPort.Handle(customerId);
        return Results.Ok(presenter.Invoices);
    }
}
