namespace Microsoft.AspNetCore.Builder;

public static class CustomerSearchController
{
    public static WebApplication UseCustomerSearchController(
        this WebApplication app)
    {
        app.MapGet(Endpoints.CustomerSearch, SearchCustomers);
        return app;
    }

    public static async Task<IResult> SearchCustomers(
        string? customerId,
        string? companyName,
        string? contactName,
        string? searchTerm,
        int? page,
        int? pageSize,
        ICustomerSearchInputPort inputPort,
        ICustomerSearchOutputPort presenter)
    {
        var criteria = new CustomerSearchCriteriaDto(
            customerId,
            companyName,
            contactName,
            searchTerm,
            page ?? 1,
            pageSize ?? 10);

        await inputPort.Handle(criteria);
        return Results.Ok(presenter.Result);
    }
}
