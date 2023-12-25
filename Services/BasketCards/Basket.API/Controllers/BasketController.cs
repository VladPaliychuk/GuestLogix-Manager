using System.Net;
using BasketCards.API.Entities;
using BasketCards.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasketCards.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{userName}, {role}", Name = "GetCardListAsync")]
        [ProducesResponseType(typeof(CardList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CardList>> GetCardListAsync(string userName, string role)
        {
            var basket = await _repository.GetCardListAsync(userName);
            return Ok(basket ?? new CardList(role, userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CardList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CardList>> UpdateCardListAsync([FromBody] CardList cardList)
        {
            return Ok(await _repository.UpdateCardListAsync(cardList));
        }

        [HttpDelete("{userName}, {role}", Name = "DeleteCardListAsync")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCardListAsync(string userName, string role)
        {
            if (role == null) throw new ArgumentNullException("Role is null");
            //if (role != "Director") throw new ArgumentException($"Can be deleted only by Director. Your role: {role}");
            else
            {
                await _repository.DeleteCardListAsync(userName);
                return Ok();
            }
        }
    }
}
