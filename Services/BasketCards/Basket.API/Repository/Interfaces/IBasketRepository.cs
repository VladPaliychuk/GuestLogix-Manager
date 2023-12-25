using BasketCards.API.Entities;

namespace BasketCards.API.Repository.Interfaces
{
    public interface IBasketRepository
    {
        Task<CardList> GetCardListAsync(string userName);
        Task<CardList> UpdateCardListAsync(CardList cardList);
        Task DeleteCardListAsync(string userName);
    }
}
