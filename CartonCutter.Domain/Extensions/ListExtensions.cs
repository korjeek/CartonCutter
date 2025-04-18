using CartonCutter.Domain.Models;

namespace CartonCutter.Domain.Extensions;

public static class ListExtensions
{
    public static void AddOrderWithNewAmount(this List<Order> orders, Order order, int orderId, int amount)
    {
        orders.Add(new Order(
            orderId,
            order.CustomerName,
            order.Nomenclature,
            order.Characteristic,
            order.Mark,
            order.Length,
            order.Width,
            order.Height,
            order.ShippingDate,
            order.WorkPieceLength,
            order.WorkPieceWidth,
            order.AmountProductsOnStamp,
            amount));
    }

    public static void AddEmptyOrder(this List<Order> orders)
    {
        orders.Add(new Order(
            -1,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            DateOnly.MinValue,
            -1,
            -1,
            string.Empty,
            -1));
    }
}