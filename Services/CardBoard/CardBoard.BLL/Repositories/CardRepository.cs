using CardBoard.BLL.Interfaces.IRepositories;
using CardBoard.DAL.Entities;
using CardBoard.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CardBoard.BLL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ICardBoardContext _context;
        public CardRepository(ICardBoardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await _context
                            .Cards
                            .Find(c => true)
                            .ToListAsync();
        }
        public async Task<Card> GetByIdAsync(string id)
        {
            return await _context
                           .Cards
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Card>> GetCardByNameAsync(string name)
        {
            //FilterDefinition<Card> filter = Builders<Card>.Filter.ElemMatch(p => p.FirstName, name);
            FilterDefinition<Card> filter = Builders<Card>.Filter.Or(
                Builders<Card>.Filter.Regex("FirstName", new BsonRegularExpression(name, "i")),
                Builders<Card>.Filter.Regex("LastName", new BsonRegularExpression(name, "i")),
                Builders<Card>.Filter.Regex("MiddleName", new BsonRegularExpression(name, "i"))
            );
            return await _context.Cards
                                 .Find(filter)
                                 .ToListAsync();

        }
        public async Task<IEnumerable<Card>> GetCardByCityAsync(string city)
        {
            FilterDefinition<Card> filter = Builders<Card>.Filter.Eq(p => p.City, city);
            return await _context
                                .Cards
                                .Find(filter)
                                .ToListAsync();
        }
        public async Task AddAsync(Card card)
        {
            card.Id= Guid.NewGuid().ToString();
            await _context.Cards.InsertOneAsync(card);
        }
        public async Task<bool> UpdateAsync(Card card)
        {
            var updateResult = await _context
                                        .Cards
                                        .ReplaceOneAsync(filter: g => g.Id == card.Id, replacement: card);
            return updateResult.IsAcknowledged
                                && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Card> filter = Builders<Card>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                                            .Cards
                                                            .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                            && deleteResult.DeletedCount > 0;
        }
    }
}
