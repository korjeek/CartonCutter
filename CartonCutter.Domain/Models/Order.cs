namespace CartonCutter.Domain.Models;

// ключевые поля, которые используются в сортировке:
// Mark, ShippingDate, WorkPieceLength, WorkPieceWidth, Amount
public class Order(
    int id,
    string customerName,
    string nomenclature,
    string characteristic,
    string mark,
    string length,
    string width,
    string height,
    DateOnly shippingDate,
    int workPieceLength,
    int workPieceWidth,
    string amountProductsOnStamp,
    int amount)
{
    public int Id { get; } = id;
    public string CustomerName { get; } = customerName;
    public string Nomenclature { get; } = nomenclature;
    public string Characteristic { get; } = characteristic;
    public string Mark { get; } = mark;
    public string Length { get; } = length;
    public string Width { get; } = width;
    public string Height { get; } = height;
    public DateOnly ShippingDate { get; } = shippingDate;
    public int WorkPieceLength { get; } = workPieceLength;
    public int WorkPieceWidth { get; } = workPieceWidth;
    public string AmountProductsOnStamp { get; } = amountProductsOnStamp;
    public int Amount { get; } = amount;
}