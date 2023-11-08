namespace BookStore.Book.Model
{
    public class UpdateModel
    {
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int QuantityAvailable { get; set; }
        public float ActualPrice { get; set; }
        public float DiscountedPrice { get; set; }
    }
}
