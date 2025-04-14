using System.Runtime.CompilerServices;
using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public static class Algorithm
{
    private static Order[] orders;
    

    public static void SetOrders(Order[] orders)
    {
        Algorithm.orders = orders;
    }
}