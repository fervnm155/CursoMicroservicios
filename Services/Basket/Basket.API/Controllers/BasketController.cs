using Basket.API.Entities;
using Basket.API.Repositories;
using Existance.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly ExistanceService.ExistanceServiceClient existanceService;

        public BasketController(IBasketRepository basketRepository, ExistanceService.ExistanceServiceClient existanceService)
        {
            this.basketRepository = basketRepository;
            this.existanceService = existanceService;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await basketRepository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            foreach(var item in shoppingCart.shoppingCartItemsItems)
            {
                var existance = existanceService.CheckExistance(new CheckExistanceRequest { Id = item.ProductId });
                if (existance.Existance < item.Quantity)
                {
                    throw new Exception("No hay existencia de este articulo");
                }
            }
            await basketRepository.UpdateBasket(shoppingCart);

            return Ok(shoppingCart);
        }
    }
}
