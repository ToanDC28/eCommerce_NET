namespace Domain.Entity;

public class Order
{
    public int Id { get; set; }
    public int ClientID { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public double TotalCost { get; set; }
    public DateTime Date {  get; set; } = DateTime.Now;

}
