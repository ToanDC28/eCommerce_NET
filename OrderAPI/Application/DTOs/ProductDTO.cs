using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public double Price { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
