namespace trackingAPI.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public DateTime OrderDate { get; set; }
        public IList<Item> Items { get; set; }


    }
}
