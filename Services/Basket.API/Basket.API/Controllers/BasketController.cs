using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using Existence.Grpc.Protos;
using MassTransit;
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
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public BasketController(IBasketRepository repository, 
            ExistenceService.ExistenceServiceClient existenceService,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            this.repository = repository;
            this.existenceService = existenceService;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout checkout) {
            var basket = await repository.GetBasket(checkout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }


            ///enviar el mensaje a rabbitMq
            var eventMessage = mapper.Map<BasketCheckoutEvent>(checkout);
            await publishEndpoint.Publish(eventMessage);

            await repository.DeleteBasket(basket.UserName);

            return Accepted();

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

            //foreach (var item in basket.Items)
            //{
            //    if ((await existenceService.CheckExistenceAsync(new 
            //        ProductRequest { Id = item.ProductId })).ProductQty <= 0)
            //    {
            //        throw new System.Exception("Producto sin existencia");
            //    }
            //}

            return Ok(await repository.UpdateBasket(basket));
        }


    }
}
