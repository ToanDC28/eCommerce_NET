namespace Domain.Entity;

public class OrderDetail
{
    public int Id { get; set; }
    public int ClientID { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Cost { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
