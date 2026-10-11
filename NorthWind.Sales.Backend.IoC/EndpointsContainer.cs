using NorthWind.Membership.Backend.Core;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointsContainer
{
    public static WebApplication MapNorthWindSalesEndpoints(
        this WebApplication app)
    {
        app.UseCreateOrderController();
        app.UseCustomerSearchController();
        app.UseProductSearchController();
        app.UseOrderInvoiceController();
        app.UseMembershipEndpoints();
        return app;
    }
}
