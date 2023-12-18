using CardBoard.BLL.Interfaces.IRepositories;
using CardBoard.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardBoard.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CardBoardController : ControllerBase
    {
        private readonly ICardRepository _repository;
        private readonly ILogger<CardBoardController> _logger;

        public CardBoardController(ICardRepository repository, 
                ILogger<CardBoardController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Card>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Card>>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetCardByIdAsync")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Card>> GetCardByIdAsync(string id)
        {
            var card = await _repository.GetByIdAsync(id);

            if(card == null)
            {
                _logger.LogError($"Card with id: {id}, not found.");
                return NotFound();
            }
            return Ok(card);
        }

        [Route("[action]/{city}", Name = "GetCardByCityAsync")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Card>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardByCityAsync(string city)
        {
            var cards = await _repository.GetCardByCityAsync(city);

            return Ok(city);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Card>> CreateProduct([FromBody] Card card)
        {
            card.Id = (new Guid(card.Id)).ToString();
            await _repository.AddAsync(card);
            return CreatedAtRoute("GetProduct", new { id = card.Id }, card);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] Card card)
        {
            return Ok(await _repository.UpdateAsync(card));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCardById(string id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
