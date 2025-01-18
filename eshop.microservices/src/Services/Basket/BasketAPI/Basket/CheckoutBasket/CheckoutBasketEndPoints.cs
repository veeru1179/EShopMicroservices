using BasketAPI.Basket.StoreBasket;

namespace BasketAPI.Basket.CheckoutBasket
{
    public record CheckoutBaskerRequest(BasketCheckoutDto BasketCheckoutDto);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBaskerRequest request, ISender sender) =>
            {
                var commnad = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(commnad);

                var response = result.Adapt<CheckoutBasketResponse>();
                return Results.Ok(response);

            })
              .WithName("CheckoutBasket ")
             .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithDescription("check out basket")
             .WithSummary("checkout Summary");
        }
    }
}
