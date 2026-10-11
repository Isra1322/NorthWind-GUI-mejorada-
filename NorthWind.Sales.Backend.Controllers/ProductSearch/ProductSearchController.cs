namespace Microsoft.AspNetCore.Builder;

public static class ProductSearchController
{
    public static WebApplication UseProductSearchController(
        this WebApplication app)
    {
        app.MapGet(Endpoints.ProductAvailable, SearchAvailableProducts);
        return app;
    }

    public static async Task<IResult> SearchAvailableProducts(
        int? productId,
        string? productName,
        string? searchTerm,
        int? page,
        int? pageSize,
        IProductSearchInputPort inputPort,
        IProductSearchOutputPort presenter)
    {
        var criteria = new ProductSearchCriteriaDto(
            productId,
            productName,
            searchTerm,
            page ?? 1,
            pageSize ?? 10);

        await inputPort.Handle(criteria);
        return Results.Ok(presenter.Result);
    }
}
