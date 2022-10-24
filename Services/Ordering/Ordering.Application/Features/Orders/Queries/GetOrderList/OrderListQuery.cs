using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class OrderListQuery : IRequest<List<OrdersViewModel>>
    {
        public string UserName { get; set; } = null!;

        public OrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
