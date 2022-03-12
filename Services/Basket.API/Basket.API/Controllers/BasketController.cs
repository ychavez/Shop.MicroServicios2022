using Basket.API.Entities;
using Basket.API.Repositories;
using Existence.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly ExistenceService.ExistenceServiceClient existenceService;

        public BasketController(IBasketRepository repository, 
            ExistenceService.ExistenceServiceClient existenceService )
        {
            this.repository = repository;
            this.existenceService = existenceService;
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await repository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {

            foreach (var item in basket.Items)
            {
                if ((await existenceService.CheckExistenceAsync(new 
                    ProductRequest { Id = item.ProductId })).ProductQty <= 0)
                {
                    throw new System.Exception("Producto sin existencia");
                }
            }

            return Ok(await repository.UpdateBasket(basket));
        }


    }
}
