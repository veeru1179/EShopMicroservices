

namespace BasketAPI.Basket.GetBasket
{
    // public record GetBasketRequest(string username);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoints
    : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                // var query = request.Adapt<GetBasketQuery>();
                var result = await sender.Send(new GetBasketQuery(username));
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("GetShoppingBasket ")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Get Shopping basket")
            .WithSummary("Basket Summary");
        }
    }
}
