using BasketProducts.API.Entities;

namespace BasketProducts.API.Repository.Interfaces
{
    public interface IProductRepository
    {
        //Task<ProductList> GetBasketAsync(string userName);
        //Task<ProductList> UpdateBasketAsync(ProductList productList);
        //Task DeleteBasket(string userName);

        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(string id);

        Task Post(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}
