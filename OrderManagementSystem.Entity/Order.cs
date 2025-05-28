namespace OrderManagementSystem.Entity
{
    public class Order : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
