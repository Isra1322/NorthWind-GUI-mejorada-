namespace NorthWind.Sales.Frontend.Views.ViewModels.CreateOrder;

public class CreateOrderViewModel(
    ICreateOrderGateway gateway,
    IModelValidatorHub<CreateOrderViewModel> validator)
{
    public IModelValidatorHub<CreateOrderViewModel> Validator => validator;
    public ModelValidator<CreateOrderViewModel>? ModelValidatorComponentReference { get; set; }

    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string ShipAddress { get; set; } = string.Empty;
    public string ShipCity { get; set; } = string.Empty;
    public string ShipCountry { get; set; } = string.Empty;
    public string ShipPostalCode { get; set; } = string.Empty;

    public List<CreateOrderDetailViewModel> OrderDetails { get; set; } = [];

    public string InformationMessage { get; private set; } = string.Empty;
    public string WarningMessage { get; set; } = string.Empty;

    // Real-time invoice totals
    public decimal Subtotal => Math.Round(OrderDetails.Sum(d => d.Total), 2);
    public decimal VatRate => 0.15m;
    public decimal Vat => Math.Round(Subtotal * VatRate, 2);
    public decimal Total => Math.Round(Subtotal + Vat, 2);

    public void AddNewOrderDetailItem()
    {
        OrderDetails.Add(new CreateOrderDetailViewModel());
    }

    public void SetCustomer(CustomerSearchDto customer)
    {
        CustomerId = customer.CustomerId;
        CustomerName = customer.CompanyName;
        ContactName = customer.ContactName ?? string.Empty;
        ShipAddress = customer.Address ?? string.Empty;
        ShipCity = customer.City ?? string.Empty;
        ShipCountry = customer.Country ?? string.Empty;
        ShipPostalCode = customer.PostalCode ?? string.Empty;
        WarningMessage = string.Empty;
    }

    public bool AddOrIncrementProduct(ProductSearchDto product)
    {
        var existing = OrderDetails.FirstOrDefault(d => d.ProductId == product.ProductId);
        if (existing != null)
        {
            if (existing.Quantity < product.UnitsInStock)
            {
                existing.Quantity++;
                WarningMessage = string.Empty;
                return true;
            }
            else
            {
                WarningMessage = $"No es posible agregar más unidades de '{product.ProductName}'. Se alcanzó el stock máximo disponible ({product.UnitsInStock}).";
                return false;
            }
        }
        else
        {
            OrderDetails.Add(new CreateOrderDetailViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Quantity = 1
            });
            WarningMessage = string.Empty;
            return true;
        }
    }

    public bool ReplaceProduct(CreateOrderDetailViewModel oldItem, ProductSearchDto newProduct)
    {
        if (oldItem.ProductId == newProduct.ProductId)
        {
            return true;
        }

        var existing = OrderDetails.FirstOrDefault(d => d.ProductId == newProduct.ProductId && d != oldItem);
        if (existing != null)
        {
            short combined = (short)(existing.Quantity + oldItem.Quantity);
            if (combined <= newProduct.UnitsInStock)
            {
                existing.Quantity = combined;
            }
            else
            {
                existing.Quantity = newProduct.UnitsInStock;
                WarningMessage = $"La cantidad combinada de '{newProduct.ProductName}' fue ajustada al límite de stock ({newProduct.UnitsInStock}).";
            }
            OrderDetails.Remove(oldItem);
        }
        else
        {
            oldItem.ProductId = newProduct.ProductId;
            oldItem.ProductName = newProduct.ProductName;
            oldItem.UnitPrice = newProduct.UnitPrice;
            oldItem.UnitsInStock = newProduct.UnitsInStock;
            if (oldItem.Quantity > newProduct.UnitsInStock)
            {
                oldItem.Quantity = newProduct.UnitsInStock;
                WarningMessage = $"La cantidad de '{newProduct.ProductName}' fue ajustada al stock disponible ({newProduct.UnitsInStock}).";
            }
        }
        return true;
    }

    public void RemoveOrderDetailItem(CreateOrderDetailViewModel item)
    {
        OrderDetails.Remove(item);
        WarningMessage = string.Empty;
    }

    public void IncrementQuantity(CreateOrderDetailViewModel item)
    {
        if (item.Quantity < item.UnitsInStock)
        {
            item.Quantity++;
            WarningMessage = string.Empty;
        }
        else
        {
            WarningMessage = $"Stock máximo alcanzado para '{item.ProductName}' ({item.UnitsInStock}).";
        }
    }

    public void DecrementQuantity(CreateOrderDetailViewModel item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity--;
            WarningMessage = string.Empty;
        }
    }

    public void UpdateQuantity(CreateOrderDetailViewModel item, short value)
    {
        if (value < 1)
        {
            item.Quantity = 1;
            WarningMessage = string.Empty;
        }
        else if (item.UnitsInStock > 0 && value > item.UnitsInStock)
        {
            item.Quantity = item.UnitsInStock;
            WarningMessage = $"No se puede superar el stock disponible de '{item.ProductName}' ({item.UnitsInStock}).";
        }
        else
        {
            item.Quantity = value;
            WarningMessage = string.Empty;
        }
    }

    public void Reset()
    {
        CustomerId = string.Empty;
        CustomerName = string.Empty;
        ContactName = string.Empty;
        ShipAddress = string.Empty;
        ShipCity = string.Empty;
        ShipCountry = string.Empty;
        ShipPostalCode = string.Empty;
        OrderDetails.Clear();
        WarningMessage = string.Empty;
    }

    public async Task Send()
    {
        InformationMessage = string.Empty;
        WarningMessage = string.Empty;

        if (OrderDetails.Count == 0)
        {
            WarningMessage = "Debe agregar al menos un producto a la orden.";
            return;
        }

        if (OrderDetails.Any(d => d.HasStockExceeded))
        {
            WarningMessage = "Existen productos cuya cantidad supera el stock disponible en inventario.";
            return;
        }

        try
        {
            var orderId = await gateway.CreateOrderAsync((CreateOrderDto)this);
            InformationMessage = string.Format(CreateOrderMessages.CreatedOrderTemplate, orderId);
            Reset();
        }
        catch (HttpRequestException ex)
        {
            if (ex.Data.Contains("Errors"))
            {
                var errors = ex.Data["Errors"] as IEnumerable<ValidationError>;
                if (errors != null && ModelValidatorComponentReference != null)
                {
                    ModelValidatorComponentReference.AddErrors(errors);
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
