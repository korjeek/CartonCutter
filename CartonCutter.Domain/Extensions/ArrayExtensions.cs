using CartonCutter.Domain.Models;

namespace CartonCutter.Domain.Extensions;

public static class ArrayExtensions
{
    public static Order GetOrderById(this Order[] orders, int id) => orders[id - 1];
}