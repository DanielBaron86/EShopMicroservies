namespace Catalog.API.Products.GetProducts;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
            {
            var queryable = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(queryable);
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductsResults>(StatusCodes.Status201Created)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}