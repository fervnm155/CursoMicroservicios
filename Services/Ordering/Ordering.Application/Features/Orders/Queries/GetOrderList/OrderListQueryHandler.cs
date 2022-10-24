using AutoMapper;
using MediatR;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class OrderListQueryHandler : IRequestHandler<OrderListQuery, List<OrdersViewModel>>
    {
        private readonly IGenericRepository<Order> orderRepository;
        private readonly IMapper mapper;

        public OrderListQueryHandler(IGenericRepository<Order> orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public async Task<List<OrdersViewModel>> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetAsync(x => x.UserName == request.UserName);

            return mapper.Map<List<OrdersViewModel>>(orders);
        }
    }
}
