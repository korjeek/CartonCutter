namespace CartonCutter.Domain.Models;

// Штука, из которой можно получить список заказов для сортировки - использовать GetOrders()
public class OrderForms
{
    private List<OrderForm> orderForms { get; set; } = [];

    public void Add(OrderForm orderForm) => orderForms.Add(orderForm);

    public Order[] GetOrders()
    {
        var orders = new List<Order>();
        foreach (var orderForm in orderForms)
        {
            var order = new Order(
                orderForm.OrderFields[0],
                orderForm.OrderFields[1],
                orderForm.OrderFields[2],
                orderForm.OrderFields[3],
                orderForm.OrderFields[4],
                orderForm.OrderFields[5],
                orderForm.OrderFields[6],
                DateOnly.Parse(orderForm.OrderFields[7]),
                int.Parse(orderForm.OrderFields[8]),
                int.Parse(orderForm.OrderFields[9]),
                orderForm.OrderFields[10],
                int.Parse(orderForm.OrderFields[11])
                );
            orders.Add(order);
        }

        return orders.ToArray();
    }
}