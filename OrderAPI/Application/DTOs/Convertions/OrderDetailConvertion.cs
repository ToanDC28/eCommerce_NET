using Domain.Entity;

namespace Application.DTOs.Convertions;

public static class OrderDetailConvertion
{
    public static OrderDetail ToEntity(OrderDetailDTO orderDetailDTO)
    {
        return new OrderDetail
        {
            Id = orderDetailDTO.Id,
            ClientID = orderDetailDTO.ClientID,
            Date = orderDetailDTO.Date,
            OrderId = orderDetailDTO.OrderId,
            ProductId = orderDetailDTO.ProductId,
            Quantity = orderDetailDTO.Quantity
        };
    }
    public static OrderDetailDTO ToDTO(OrderDetail orderDetail, ProductDTO product, UserDTO user)
    {
        return new OrderDetailDTO
        {
            Status = orderDetail.Status,
            Id = orderDetail.Id,
            Quantity = orderDetail.Quantity,
            ClientName = user.Name,
            ClientID = user.Id,
            Date = orderDetail.Date,
            OrderId= orderDetail.OrderId,
            ProductId = product.Id,
            Price = product.Price,
            ProductDescription = product.Description,
            ProductName = product.Name,
            TotalCost = orderDetail.Cost,
        };
    }
}
