namespace NorthWind.Sales.Frontend.Views.Components;

public partial class CreateOrderComponent
{
    [Parameter]
    public CreateOrderViewModel Order { get; set; } = default!;

    private bool IsCustomerModalOpen { get; set; }
    private bool IsProductModalOpen { get; set; }
    private string ProductModalTitle { get; set; } = "Buscar Producto Disponible";
    private CreateOrderDetailViewModel? ItemToReplace { get; set; }

    private void OpenCustomerModal()
    {
        IsCustomerModalOpen = true;
    }

    private void OpenAddProductModal()
    {
        ItemToReplace = null;
        ProductModalTitle = "Buscar Producto Disponible";
        IsProductModalOpen = true;
    }

    private void OpenChangeProductModal(CreateOrderDetailViewModel item)
    {
        ItemToReplace = item;
        ProductModalTitle = $"Cambiar Producto '{item.ProductName}'";
        IsProductModalOpen = true;
    }

    private void OnCustomerSelected(CustomerSearchDto customer)
    {
        Order.SetCustomer(customer);
    }

    private void OnProductSelected(ProductSearchDto product)
    {
        if (ItemToReplace != null)
        {
            Order.ReplaceProduct(ItemToReplace, product);
            ItemToReplace = null;
        }
        else
        {
            Order.AddOrIncrementProduct(product);
        }
    }

    private void OnQuantityChanged(CreateOrderDetailViewModel item, ChangeEventArgs e)
    {
        if (short.TryParse(e.Value?.ToString(), out short parsedQuantity))
        {
            Order.UpdateQuantity(item, parsedQuantity);
        }
        else
        {
            Order.UpdateQuantity(item, 1);
        }
    }
}
