
using BasketAPI.Basket.GetBasket;


namespace BasketAPI.Basket.DeleteBasket
{

    //  public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool isSuccess);
    public class DeleteBasketEndPoints
    : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                // var commnad = request.Adapt<DeleteBasketCommnad>();
                var result = await sender.Send(new DeleteBasketCommnad(userName));
               result.Adapt<DeleteBasketResponse>();
                return Results.Ok(true);
            })
            .WithName("DeleteBasket ")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Delete basket")
            .WithSummary("Delete Summary");

        }
    }
}
