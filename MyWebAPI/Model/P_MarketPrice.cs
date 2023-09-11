namespace MyWebAPI.Model
{
    public class P_MarketPrice
    {
        public string ProductID { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }

        public int Capacity { get; set; }
        public double Price_100g { get; set; }
    }
}
