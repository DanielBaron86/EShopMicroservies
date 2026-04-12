namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserNamer);

public record DeletebasketResponse(bool IsSuccess);
public class DeleteBasketEndpoints :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{UserName}", async (ISender sender, string UserName) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(UserName));
                var response = result.Adapt<DeletebasketResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeletebasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}