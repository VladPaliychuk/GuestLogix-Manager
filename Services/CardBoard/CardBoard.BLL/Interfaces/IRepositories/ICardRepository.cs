using CardBoard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBoard.BLL.Interfaces.IRepositories
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Task<IEnumerable<Card>> GetCardByNameAsync(string name);
        Task<IEnumerable<Card>> GetCardByCityAsync(string city);
    }
}
