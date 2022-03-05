using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(d => d.Id).NotEqual(0)
                .WithMessage("No se puede borrar el registro 0")
                .NotNull()
                .WithMessage("No se puede eliminar numero null")
            .NotEqual(10);
                
        }
    }
}
