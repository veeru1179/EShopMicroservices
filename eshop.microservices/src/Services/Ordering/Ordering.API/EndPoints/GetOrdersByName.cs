
using Ordering.Application.Queries.GetOrdersByName;

namespace Ordering.API.EndPoints
{
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = sender.Send(new GetOrdersByNameQuery(orderName));
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByName")
                .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Orders ByName")
                .WithSummary("Get Orders ByName\"");
        }
    }
}
