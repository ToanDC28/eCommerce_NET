using Domain.Entity;

namespace Application.DTOs.Convertions;

public static class OrderConvertion
{
    public static Order ToEntity(OrderDTO order)
    {
        return new Order
        {
            Address = order.Address!,
            ClientID = order.ClientID,
            Date = order.Date,
            Id = order.Id,
            Phone = order.Phone,
            TotalCost = order.TotalCost,
        };
    }

    public static (OrderDTO, IEnumerable<OrderDTO>) ToDTO(OrderDTO order, IEnumerable<Order>)
    {

    }
}
