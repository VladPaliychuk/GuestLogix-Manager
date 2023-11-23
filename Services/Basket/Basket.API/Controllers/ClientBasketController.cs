using Basket.API.Entities;
using Basket.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ClientBasketController : ControllerBase
    {
        private readonly IClientBasketRepository _repository;

        public ClientBasketController(IClientBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{userName}, {role}", Name = "GetProductListAsync")]
        [ProducesResponseType(typeof(ProductList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductList>> GetProductListAsync(string userName, string role)
        {
            var basket = await _repository.GetBasketAsync(userName);
            return Ok(basket ?? new ProductList(userName, role));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductList>> UpdateProductListAsync([FromBody] ProductList cardList)
        {
            return Ok(await _repository.UpdateBasketAsync(cardList));
        }

        [HttpDelete("{userName}, {role}", Name = "DeleteProductListAsync")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductListAsync(string userName, string role)
        {
            if (role == null) throw new ArgumentNullException("Role is null");
            else
            {
                await _repository.DeleteBasket(userName);
                return Ok();
            }
        }
    }
}
