namespace NorthWind.Sales.Frontend.Views.ViewModels.CreateOrder;

public class CreateOrderViewModel(
    ICreateOrderGateway gateway,
    IModelValidatorHub<CreateOrderViewModel> validator)
{
    public IModelValidatorHub<CreateOrderViewModel> Validator => validator;
    public ModelValidator<CreateOrderViewModel>? ModelValidatorComponentReference { get; set; }

    public string CustomerId { get; set; } = string.Empty;
    public string ShipAddress { get; set; } = string.Empty;
    public string ShipCity { get; set; } = string.Empty;
    public string ShipCountry { get; set; } = string.Empty;
    public string ShipPostalCode { get; set; } = string.Empty;
    public List<CreateOrderDetailViewModel> OrderDetails { get; set; } = [];

    public string InformationMessage { get; private set; } = string.Empty;

    public void AddNewOrderDetailItem()
    {
        OrderDetails.Add(new CreateOrderDetailViewModel());
    }

    public async Task Send()
    {
        InformationMessage = "";
        try
        {
            var OrderId = await gateway.CreateOrderAsync((CreateOrderDto)this);
            InformationMessage = string.Format(CreateOrderMessages.CreatedOrderTemplate, OrderId);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Data.Contains("Errors"))
            {
                var Errors = ex.Data["Errors"] as IEnumerable<ValidationError>;
                if (Errors != null && ModelValidatorComponentReference != null)
                {
                    ModelValidatorComponentReference.AddErrors(Errors);
                }
            }
            else
            {
                throw;
            }
        }
    }

    public static explicit operator CreateOrderDto(CreateOrderViewModel model) =>
        new CreateOrderDto(
            model.CustomerId, model.ShipAddress, model.ShipCity,
            model.ShipCountry, model.ShipPostalCode,
            model.OrderDetails.Select(d => new CreateOrderDetailDto(
                d.ProductId, d.UnitPrice, d.Quantity)));
}
