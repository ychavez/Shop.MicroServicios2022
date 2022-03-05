using FluentValidation;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListValidator: AbstractValidator<GetOrdersListQuery>
    {
        public GetOrdersListValidator()
        {
            RuleFor(g => g.UserName).NotEqual("Admin");
        }
    }
}
