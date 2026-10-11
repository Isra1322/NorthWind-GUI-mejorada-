namespace NorthWind.Sales.Entities.Dtos.CustomerSearch;

public class CustomerSearchDto(
    string customerId,
    string companyName,
    string? contactName,
    string? address,
    string? city,
    string? country,
    string? postalCode)
{
    public string CustomerId => customerId;
    public string CompanyName => companyName;
    public string? ContactName => contactName;
    public string? Address => address;
    public string? City => city;
    public string? Country => country;
    public string? PostalCode => postalCode;
}
