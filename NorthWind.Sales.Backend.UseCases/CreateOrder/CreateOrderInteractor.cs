namespace NorthWind.Sales.Backend.UseCases.CreateOrder;

internal class CreateOrderInteractor(
    ICreateOrderOutputPort outputPort,
    ICommandsRepository repository,
    IModelValidatorHub<CreateOrderDto> modelValidatorHub,
    IDomainEventHub<SpecialOrderCreatedEvent> eventHub,
    IDomainLogger logger,
    IDomainTransaction transaction,
    IUserService userService) : ICreateOrderInputPort
{
    public async Task Handle(CreateOrderDto orderDto)
    {
        GuardUser.AgainstUnauthenticated(userService);

        await GuardModel.AgainstNotValid(modelValidatorHub, orderDto);

        OrderAggregate Order = OrderAggregate.From(orderDto);

        try
        {
            transaction.BeginTransaction();

            await repository.CreateOrder(Order);
            await repository.SaveChanges();

            await logger.Log(new DomainLog(
                DateTime.Now,
                userService.UserName,
                string.Format(CreateOrderMessages.CreatedOrderLogTemplate, Order.Id, orderDto.CustomerId)));

            if (new SpecialOrderSpecification().IsSatisfiedBy(Order))
            {
                await eventHub.Raise(new SpecialOrderCreatedEvent(
                    Order.Id, Order.OrderDetails.Count));
            }

            transaction.CommitTransaction();

            await outputPort.Handle(Order);
        }
        catch
        {
            transaction.RollbackTransaction();

            await logger.Log(new DomainLog(
                DateTime.Now,
                userService.UserName,
                string.Format(CreateOrderMessages.OrderCreationCancelledTemplate, Order.Id)));

            throw;
        }
    }
}
