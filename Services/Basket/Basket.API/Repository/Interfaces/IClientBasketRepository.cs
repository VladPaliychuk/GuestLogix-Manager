using Basket.API.Entities;

namespace Basket.API.Repository.Interfaces
{
    public interface IClientBasketRepository 
    {
        Task<ProductList> GetBasketAsync(string userName);
        Task<ProductList> UpdateBasketAsync(ProductList productList);
        Task DeleteBasket(string userName);
    }
}
