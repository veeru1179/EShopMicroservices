namespace Ordering.Application.Orders.Commands.UpdataOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {

            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is Required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is Required");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Ordername is Required");
        }
    }


}
