namespace Application.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public int ClientID { get; set; }
    public string? ClientName { get; set; }
    public string? Address { get; set; }
    public string Phone { get; set; }
    public double TotalCost { get; set; }
    public DateTime Date { get; set; }
    public List<OrderDetailDTO>? Items { get; set; }
}
