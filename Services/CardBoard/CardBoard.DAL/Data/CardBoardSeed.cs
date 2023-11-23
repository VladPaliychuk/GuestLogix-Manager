using CardBoard.DAL.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBoard.DAL.Data
{
    public class CardBoardSeed
    {
        public static void SeedData(IMongoCollection<Card> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredCards());
            }
        }

        private static IEnumerable<Card> GetPreconfiguredCards()
        {
            return new List<Card>()
            {
                new Card()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Number = 123456,
                    FirstName = "Oleg",
                    LastName = "Bogutskiy",
                    MiddleName = "Olexandrovich",
                    PhoneNumber = "3809999999",
                    Mail = "bego@gmail.com",
                    DateOfBirth = DateTime.Parse("12.10.2023"),
                    Age = "0",
                    City = "Chernivtsi",
                    Bonuses = 123
                },
            };
        }
    }
}
