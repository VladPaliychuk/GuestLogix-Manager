using System.Net;
using BasketProducts.API.Entities;
using BasketProducts.API.Repository.Interfaces;
using BasketProducts.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasketProducts.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        ICachingService _cachingService;
        public ProductController(IProductRepository repository, ICachingService cachingService)
        {
            _cachingService = cachingService ?? throw new ArgumentNullException(nameof(cachingService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var cacheproducts = _cachingService.GetData<IEnumerable<Product>>("products");

            if (cacheproducts != null)
            {
                Console.WriteLine("Getting from Cache");
                return Ok(cacheproducts.ToList());
            }

            var productsDb = await _repository.GetAll();


            _cachingService.SetData("products", productsDb.ToList(), DateTimeOffset.Now.AddMinutes(20));
            Console.WriteLine("Getting from BD");
            return Ok(productsDb.ToList());
        }

        [HttpPost("Post")]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            product.Id = Guid.NewGuid().ToString();

            await _repository.Post(product);
            return Ok();
        }
    }
}
