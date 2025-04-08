namespace CartonCutter.Domain.Models;

// ключевые поля, которые используются в сортировке:
// Mark, ShippingDate, WorkPieceLength, WorkPieceWidth, Amount
public class Order(
    string? CustomerName, 
    string? Nomenclature, 
    string? Characteristic, 
    string? Mark,
    string? Length,
    string? Width,
    string? Height,
    DateOnly ShippingDate,
    int WorkPieceLength,
    int WorkPieceWidth,
    string? AmountProductsOnStamp,
    int Amount)
{
    public string? CustomerName { get; init; }
    public string? Nomenclature { get; init; }
    public string? Characteristic { get; init; }
    public string? Mark { get; init; }
    public string? Length { get; init; }
    public string? Width { get; init; }
    public string? Height { get; init; }
    public DateOnly ShippingDate { get; init; }
    public int WorkPieceLength { get; init; }
    public int WorkPieceWidth { get; init; }
    public string? AmountProductsOnStamp { get; init; }
    public int Amount { get; init; }
}