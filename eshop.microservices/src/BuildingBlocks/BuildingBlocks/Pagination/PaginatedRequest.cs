﻿

namespace BuildingBlocks.Pagination
{
    public record PaginatedRequest(int pageIndex = 0, int pageSize = 10);
}
