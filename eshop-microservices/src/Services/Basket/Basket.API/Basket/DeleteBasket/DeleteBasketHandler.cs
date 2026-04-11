namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeletebasketResult>;
public record DeletebasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
    }
}


public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand,DeletebasketResult>
{
    public async Task<DeletebasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //TODO delete from DB and Cache
        //session.Delete<Product>(command.Id); 
        return new DeletebasketResult(true);

    }
}