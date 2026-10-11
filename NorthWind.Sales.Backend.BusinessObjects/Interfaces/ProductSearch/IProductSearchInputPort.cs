namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.ProductSearch;

public interface IProductSearchInputPort
{
    Task Handle(ProductSearchCriteriaDto criteria);
}
