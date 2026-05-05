using Microsoft.OpenApi.Models;

namespace Catalog.API.Products.GetProductById;
public record GetProductbyIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductbyIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductbyIdResponse>(StatusCodes.Status201Created)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id")
        .RequireAuthorization()
        .WithOpenApi(op => {
            op.Security = new List<OpenApiSecurityRequirement>
            {
                new() { [new OpenApiSecurityScheme { Reference = new() { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] = Array.Empty<string>() }
            };
            return op;
        })
        ;
    }
}