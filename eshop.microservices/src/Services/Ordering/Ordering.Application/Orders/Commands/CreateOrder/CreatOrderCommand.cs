﻿

namespace Ordering.Application.Orders.Commands.CreateOrder
{

    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreatOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreatOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is Required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems is Required");
        }
    }

}
