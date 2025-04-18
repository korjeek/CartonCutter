using CartonCutter.Domain.Models;

namespace CartonCutter.Domain.Extensions;

public static class ArrayExtensions
{
    /// <summary>
    /// Extension method created for indexing orders. Gives away an <see cref="Order"/> object from orders by its id.
    /// </summary>
    /// <param name="orders">Array of orders from which the <see cref="Order"/> will be given</param>
    /// <param name="id">The order id we want to recieve</param>
    /// <returns>Object of type <see cref="Order"/></returns>
    public static Order GetOrderById(this Order[] orders, int id) => orders[id - 1];
}