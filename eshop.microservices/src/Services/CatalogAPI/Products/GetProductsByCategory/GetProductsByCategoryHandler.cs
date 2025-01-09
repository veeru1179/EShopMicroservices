﻿using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(p => p.Category.Contains(query.category)).ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
