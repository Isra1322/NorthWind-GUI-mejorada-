namespace NorthWind.Sales.Backend.SmtpGateways.EventHandlers;

internal class SpecialOrderCreatedEventHandler(
    IOptions<SmtpOptions> options) : IDomainEventHandler<SpecialOrderCreatedEvent>
{
    public async Task Handle(SpecialOrderCreatedEvent domainEvent)
    {
        var Smtp = options.Value;
        using var Client = new SmtpClient(Smtp.Host, Smtp.Port)
        {
            Credentials = new NetworkCredential(Smtp.UserName, Smtp.Password),
            EnableSsl = true
        };

        var Mail = new MailMessage(
            from: "noreply@northwind.com",
            to: "admin@northwind.com",
            subject: SpecialOrderCreatedMessages.EmailSubject,
            body: string.Format(SpecialOrderCreatedMessages.EmailBodyTemplate,
                domainEvent.OrderId, domainEvent.ProductsCount));

        await Client.SendMailAsync(Mail);
    }
}
