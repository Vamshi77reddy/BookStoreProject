using BookStore.Book.Entity;
using BookStore.User.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Orders.Entity
{
    public class OrderEntity
    {
        [Key]

        public string OrderId { get; set; }
        public long UserID { get; set; }
        public long BookID { get; set; }
        public long OrderQty { get; set; }
        public float OrderAmount { get; set; }
        public bool IsSuccess { get; set; }
        public string url { get; set; }

        [NotMapped ]
        public UserEntity User { get; set; }

        [NotMapped ]
        public BookEntity Book { get; set; }

    }
}
