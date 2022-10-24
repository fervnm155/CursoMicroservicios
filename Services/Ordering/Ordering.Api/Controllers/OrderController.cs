using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersViewModel>>> GetOrders([FromQuery] string UserName)
        {
            var query=new OrderListQuery(UserName);
            return await mediator.Send(query);
        }
    }
}
