namespace Basket.API.Entities
{
    public class CardList
    {
        public string Role { get; set; } = null!;
        public string UserName { get; set; } = null;
        public List<CardItem> Items { get; set; } = new List<CardItem>();

        public CardList()
        {

        }

        public CardList(string role, string userName)
        {
            UserName = userName;
            Role = role;
        }

        public int TotalCount
        {
            get
            {
                int count = 0;
                foreach(var item in Items)
                {
                    count++;
                }
                return count;
            }
        }
    }
}
