namespace CartonCutter.Domain.Models;

// ключевые поля, которые используются в сортировке:
// Mark, ShippingDate, WorkPieceLength, WorkPieceWidth, WorkPieceLength, Amount
public class Order(
    int id,
    string customerName,
    string nomenclature,
    string? characteristic,
    string mark,
    int? length,
    int? width,
    int? height,
    DateOnly shippingDate,
    int workPieceLength,
    int workPieceWidth,
    int? amountProductsOnStamp,
    int amount)
{
    public int Id { get; } = id;
    public string CustomerName { get; } = customerName;
    public string Nomenclature { get; } = nomenclature;
    public string? Characteristic { get; } = characteristic;
    public string Mark { get; } = mark;
    public int? Length { get; } = length;
    public int? Width { get; } = width;
    public int? Height { get; } = height;
    public DateOnly ShippingDate { get; } = shippingDate;
    public int WorkPieceLength { get; } = workPieceLength;
    public int WorkPieceWidth { get; } = workPieceWidth;
    public int? AmountProductsOnStamp { get; } = amountProductsOnStamp;
    public int Amount { get; } = amount;
}