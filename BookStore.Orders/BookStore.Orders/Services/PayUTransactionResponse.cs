namespace BookStore.Orders.Services
{
    public class PayUTransactionResponse
    {
        public string Mihpayid { get; set; }
        public string Mode { get; set; }
        public string BankCode { get; set; }
        public string Status { get; set; }
        public string UnmappedStatus { get; set; }
        public string Key { get; set; }
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
        public string BankRefNum { get; set; }
        public string Txnid { get; set; }
        public decimal Amount { get; set; }
        public string ProductInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }
        public string PG_TYPE { get; set; }
    }

}