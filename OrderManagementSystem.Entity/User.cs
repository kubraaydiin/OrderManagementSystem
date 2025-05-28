namespace OrderManagementSystem.Entity
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
