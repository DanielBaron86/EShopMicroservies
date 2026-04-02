namespace Catalog.API.Products.GetProducts;
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductsResults>(StatusCodes.Status201Created)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}