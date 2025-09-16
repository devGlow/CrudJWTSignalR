namespace Projet101.Models
{
    public class User
    {
        public int Id { get; set; } // AUTOINCREMENT
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Order> Orders { get; set; }
        public List<ShippingAddress> ShippingAddresses { get; set; }
    }
}
