namespace BasketProducts.API.Entities
{
    public class ProductList
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public List<Product> Products { get; set;} = new();

        public ProductList() { }

        public ProductList(string userName, string role)
        {
            Role = role;
            UserName = userName;
        }
        public decimal? TotalPrice
        {
            get
            {
                decimal? totalprice = 0;
                
                foreach (var item in Products)
                {
                    totalprice += item.Price;
                }
                return totalprice;
            }
        }
    }
}
