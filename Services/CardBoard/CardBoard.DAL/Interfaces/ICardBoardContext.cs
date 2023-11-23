using CardBoard.DAL.Entities;
using MongoDB.Driver;

namespace CardBoard.DAL.Interfaces
{
    public interface ICardBoardContext
    {
        IMongoCollection<Card> Cards { get; }
    }
}
