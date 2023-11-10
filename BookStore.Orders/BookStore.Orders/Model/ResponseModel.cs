namespace BookStore.Orders.Model
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public  bool IsSuccess { get; set; }
        public object Data { get; set; }
    }
}
