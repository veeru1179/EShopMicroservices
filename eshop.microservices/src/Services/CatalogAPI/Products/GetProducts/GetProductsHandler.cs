﻿
using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsQuery(int? pageNumber = 1, int? pageCount = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.pageNumber ?? 1, query.pageCount ?? 10, cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
