using BookStore.Orders.Entity;

namespace BookStore.Orders.Model
{
    public class PayUPaymentResponse
    {
       
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductInfo { get; set; }
        public string Amount { get; set; }
        public string Txnid { get; set; }
        public string Mihpayid { get; set; }
        public string Mode { get; set; }
        public string Status { get; set; }
        public string Unmappedstatus { get; set; }
        public string Key { get; set; }
        public string Hash { get; set; }

    }
}
