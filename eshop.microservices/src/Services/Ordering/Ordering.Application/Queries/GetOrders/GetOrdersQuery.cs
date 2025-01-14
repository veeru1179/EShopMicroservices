
using BuildingBlocks.Pagination;

namespace Ordering.Application.Queries.GetOrders
{
    public record GetOrdersQuery(PaginatedRequest PaginatedRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
}
