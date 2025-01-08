 

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid ResultId);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var commnad = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(commnad);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.ResultId}", response);
            })
             .WithName("CreateProduct")
             .Produces<CreateProductResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Create Product")
             .WithDescription("Create Product");

        }
    }
}
