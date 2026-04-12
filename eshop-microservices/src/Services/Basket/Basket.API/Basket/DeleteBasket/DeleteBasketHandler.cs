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


public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand,DeletebasketResult>
{
    public async Task<DeletebasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.Username, cancellationToken);
        return new DeletebasketResult(true);

    }
}