namespace Basket.API.Entities
{
    public class CardItem
    {
        public int Number { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Mail { get; set;} = null!;
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set;} = null!;
        public string City { get; set; } = null!;
        public int Bonuses { get; set; }
    }
}
