﻿namespace Ordering.Application.Orders.Commands.UpdataOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);

        }

        public void UpdateOrderNewValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress
               , orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.State, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress
                , orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.State, orderDto.BillingAddress.Country, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(OrderName.Of(orderDto.OrderName), updatedShippingAddress, updatedBillingAddress, updatedPayment, orderDto.Status);

        }
    }
}
