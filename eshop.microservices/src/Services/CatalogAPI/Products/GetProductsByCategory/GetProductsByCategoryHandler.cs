using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryHandler.Handle with the input {query}", query);
            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);
            if (products is null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductsByCategoryResult(products);
        }
    }
}
