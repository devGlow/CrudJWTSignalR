namespace Projet101.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
