using Domain.Entity;

namespace Application.DTOs;

public class OrderDetailDTO
{
    public int Id { get; set; }
    public int ClientID { get; set; }
    public string? ClientName { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double TotalCost { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
}