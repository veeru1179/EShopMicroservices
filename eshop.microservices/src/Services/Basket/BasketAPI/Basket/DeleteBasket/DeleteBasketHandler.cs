

namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBasketCommnad(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool isSuccess);

    public class DeleteBasketCommnadValidator : AbstractValidator<DeleteBasketCommnad>
    {
        public DeleteBasketCommnadValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can not be empty");
        }
    }
    public class DeleteBasketCommnadHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommnad, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommnad commnad, CancellationToken cancellationToken)
        {
            bool result = await repository.DeleteBasket(commnad.UserName);
            return new DeleteBasketResult(result);
        }
    }
}
