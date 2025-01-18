﻿

namespace Ordering.API.EndPoints
{
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginatedRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrders")
                .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Orders  ")
                .WithSummary("Get Orders");
        }
    }
}
