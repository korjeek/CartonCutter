namespace CartonCutter.Domain.Models;

// Класс, который описывает стартовую форму заказа, все поля заказа в нём - строки.
public class OrderForm
{
    public string[] OrderFields { get; } = new string[12];

    public OrderForm()
    {
        Array.Fill(OrderFields, "");
    }
}