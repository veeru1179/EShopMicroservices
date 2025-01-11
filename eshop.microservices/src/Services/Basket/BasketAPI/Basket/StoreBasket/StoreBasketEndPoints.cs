

using BasketAPI.Basket.GetBasket;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndPoints
    : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async ([FromBody] StoreBasketRequest request, ISender sender) =>
            {
                var commnad = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(commnad);

                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{commnad.Cart.UserName}", response);

            })
              .WithName("AddBasket ")
             .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithDescription("Get Shopping basket")
             .WithSummary("Basket Summary");
        }
    }
}
