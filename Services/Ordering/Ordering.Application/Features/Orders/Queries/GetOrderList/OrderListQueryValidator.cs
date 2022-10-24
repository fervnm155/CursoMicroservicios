using FluentValidation;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class OrderListQueryValidator : AbstractValidator<OrderListQuery>
    {
        public OrderListQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("El Username no puede ser vacio :v")
                .MinimumLength(3)
                .MaximumLength(10);
        }
    }
}
