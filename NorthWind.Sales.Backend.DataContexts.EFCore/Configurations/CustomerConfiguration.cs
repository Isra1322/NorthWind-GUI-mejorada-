namespace NorthWind.Sales.Backend.DataContexts.EFCore.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Id)
            .HasMaxLength(5)
            .IsFixedLength();
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(40);
        builder.Property(c => c.ContactName)
            .HasMaxLength(50);
        builder.Property(c => c.Address)
            .HasMaxLength(60);
        builder.Property(c => c.City)
            .HasMaxLength(30);
        builder.Property(c => c.Country)
            .HasMaxLength(30);
        builder.Property(c => c.PostalCode)
            .HasMaxLength(15);
        builder.Property(c => c.CurrentBalance)
            .HasPrecision(8, 2);
        builder.HasData(
            new Customer
            {
                Id = "ALFKI",
                Name = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                Address = "Obere Str. 57",
                City = "Berlin",
                Country = "Germany",
                PostalCode = "12209",
                CurrentBalance = 0
            },
            new Customer
            {
                Id = "ANATR",
                Name = "Ana Trujillo Emparedados y helados",
                ContactName = "Ana Trujillo",
                Address = "Avda. de la Constitucion 2222",
                City = "Mexico D.F.",
                Country = "Mexico",
                PostalCode = "05021",
                CurrentBalance = 0
            },
            new Customer
            {
                Id = "ANTON",
                Name = "Antonio Moreno Taquería",
                ContactName = "Antonio Moreno",
                Address = "Mataderos 2312",
                City = "Mexico D.F.",
                Country = "Mexico",
                PostalCode = "05023",
                CurrentBalance = 100
            });
    }
}
