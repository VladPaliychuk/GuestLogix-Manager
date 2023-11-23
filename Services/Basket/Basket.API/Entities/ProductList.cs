namespace Basket.API.Entities
{
    public class ProductList
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public List<Product> Products { get; set;} = new List<Product>();

        public ProductList() { }

        public ProductList(string userName, string role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role), "The 'role' parameter cannot be null.");
            UserName = userName;
        }
        public decimal? TotalPrice
        {
            get
            {
                decimal? totalprice = 0;
                foreach(var item in Products)
                {
                    totalprice += item.Price;
                }
                return totalprice;
            }
        }
    }
}
