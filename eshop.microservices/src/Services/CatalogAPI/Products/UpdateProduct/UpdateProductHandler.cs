﻿
using FluentValidation;
using Marten.Linq.SoftDeletes;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool isSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id should be present");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name shouldn't be empty");
            RuleFor(x => x.Name).Length(2, 150).WithMessage("Name must be between 2 to 150 length");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be more than 0");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
