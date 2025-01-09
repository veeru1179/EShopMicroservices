
using FluentValidation;

namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);

    public class DeleteProductCommnadValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommnadValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id shouldn't be empty");
        }
    }
    public class DeleteProductHandler(IDocumentSession session)
        : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync();
            return new DeleteProductResult(true);
        }
    }
}
