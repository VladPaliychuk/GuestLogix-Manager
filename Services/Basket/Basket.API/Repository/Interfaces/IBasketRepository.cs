using Basket.API.Entities;

namespace Basket.API.Repository.Interfaces
{
    public interface IBasketRepository
    {
        Task<CardList> GetCardListAsync(string userName);
        Task<CardList> UpdateCardListAsync(CardList cardList);
        Task DeleteCardListAsync(string userName);
    }
}
