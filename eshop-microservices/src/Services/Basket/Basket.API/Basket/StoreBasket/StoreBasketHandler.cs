namespace Basket.API.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string userName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
     RuleFor(x => x.Cart).NotNull().NotEmpty().WithMessage("Cart is required");
     RuleFor(x => x.Cart.UserName).NotNull().NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand,StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand commnad, CancellationToken cancellationToken)
    {
        //TODO: save basket to database(Marten Upsert)
        //TODO: update cache
        return new StoreBasketResult(commnad.Cart.UserName);
    }
}