using BuildingBlocks.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {

            var pageIndex = query.PaginatedRequest.pageIndex;
            var pageSize = query.PaginatedRequest.pageSize;
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders = await dbContext.Orders
                             .Include(x => x.OrderItems)
                             .OrderBy(x => x.OrderName.Value)
                             .Skip(pageSize * pageIndex)
                             .Take(pageSize)
                             .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));

        }
    }
}
