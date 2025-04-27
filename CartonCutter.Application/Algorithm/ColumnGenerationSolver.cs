using CartonCutter.Domain.Models;
using Kaos.Combinatorics;

namespace CartonCutter.Application.Algorithm;

public class ColumnGenerationSolver(Order[] orders, int threshold)
{
    private readonly List<Pattern> _patterns = [];

    private bool IsPassedThreshold(double waste, int totalWidth) => waste / totalWidth * 100 <= threshold;


    private void GenerateOneLinePatterns()
    {
        foreach (var piece in orders)
        {
            if (piece.WorkPieceWidth > Pattern1030.MaxWidth ||
                !IsPassedThreshold(Pattern1030.MaxWidth - piece.WorkPieceWidth, Pattern1030.MaxWidth)) continue;

            var newPattern = new Pattern1030(piece.WorkPieceLength);
            newPattern.AddOrderIdInProduction(piece.Id);

            newPattern.TimeToShip = (piece.ShippingDate.ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
            newPattern.CountLines();
            newPattern.CalculateWaste(piece.WorkPieceWidth);
            _patterns.Add(newPattern);
        }
    }

    private void GenerateTwoLinePatterns(HashSet<int> lengthsSet)
    {
        var combinationsBy2 = new Multicombination(orders.Length, 2);
        foreach (var comb in combinationsBy2.GetRows())
        {
            lengthsSet.Clear();
            var piece1 = orders[comb[0]];
            var piece2 = orders[comb[1]];
            var totalPieceWidth = piece1.WorkPieceWidth + piece2.WorkPieceWidth;

            lengthsSet.Add(piece1.WorkPieceLength);
            lengthsSet.Add(piece2.WorkPieceLength);

            // 1. Если одной длины
            if (piece1.Mark == piece2.Mark &&
                lengthsSet.Count == 1 &&
                totalPieceWidth <= Pattern1030.MaxWidth &&
                IsPassedThreshold(Pattern1030.MaxWidth - totalPieceWidth, Pattern1030.MaxWidth))
            {
                var newPattern = new Pattern1030(piece1.WorkPieceLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);

                newPattern.TimeToShip =
                    (GetMostEarliest(piece1.ShippingDate, piece2.ShippingDate)
                        .ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
                newPattern.CountLines();
                newPattern.CalculateWaste(totalPieceWidth);
                _patterns.Add(newPattern);
            }

            // 2. Если разных длин
            if (piece1.Mark == piece2.Mark &&
                lengthsSet.Count == 2 &&
                totalPieceWidth <= Pattern1380.MaxWidth &&
                IsPassedThreshold(Pattern1380.MaxWidth - totalPieceWidth, Pattern1380.MaxWidth))
            {
                var bigLength = lengthsSet.Max();
                var smallLength = lengthsSet.Min();
                var newPattern = new Pattern1380(bigLength, smallLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);

                newPattern.TimeToShip =
                    (GetMostEarliest(piece1.ShippingDate, piece2.ShippingDate)
                        .ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
                newPattern.CountLines();
                newPattern.CalculateWaste(totalPieceWidth);
                _patterns.Add(newPattern);
            }
        }
    }

    private void GenerateThreeLinePatterns(HashSet<int> lengthsSet)
    {
        var combinationsBy3 = new Multicombination(orders.Length, 3);
        foreach (var comb in combinationsBy3.GetRows())
        {
            lengthsSet.Clear();
            var piece1 = orders[comb[0]];
            var piece2 = orders[comb[1]];
            var piece3 = orders[comb[2]];
            var totalPieceWidth = piece1.WorkPieceWidth + piece2.WorkPieceWidth + piece3.WorkPieceWidth;

            lengthsSet.Add(piece1.WorkPieceLength);
            lengthsSet.Add(piece2.WorkPieceLength);
            lengthsSet.Add(piece3.WorkPieceLength);


            // 1. Если одной длины
            if (piece1.Mark == piece2.Mark &&
                piece2.Mark == piece3.Mark &&
                lengthsSet.Count == 1 &&
                totalPieceWidth <= Pattern1030.MaxWidth &&
                IsPassedThreshold(Pattern1030.MaxWidth - totalPieceWidth, Pattern1030.MaxWidth))
            {
                var newPattern = new Pattern1030(piece1.WorkPieceLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);
                newPattern.AddOrderIdInProduction(piece3.Id);

                newPattern.TimeToShip =
                    (GetMostEarliest(piece1.ShippingDate, piece2.ShippingDate, piece3.ShippingDate)
                        .ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
                newPattern.CountLines();
                newPattern.CalculateWaste(totalPieceWidth);
                _patterns.Add(newPattern);
            }

            // 2. Если разных длин
            if (piece1.Mark == piece2.Mark &&
                piece2.Mark == piece3.Mark &&
                lengthsSet.Count == 2 &&
                totalPieceWidth <= Pattern1380.MaxWidth &&
                IsPassedThreshold(Pattern1380.MaxWidth - totalPieceWidth, Pattern1380.MaxWidth))
            {
                var bigLength = lengthsSet.Max();
                var smallLength = lengthsSet.Min();
                var newPattern = new Pattern1380(bigLength, smallLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);
                newPattern.AddOrderIdInProduction(piece3.Id);

                newPattern.TimeToShip =
                    (GetMostEarliest(piece1.ShippingDate, piece2.ShippingDate, piece3.ShippingDate)
                        .ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
                newPattern.CountLines();
                newPattern.CalculateWaste(totalPieceWidth);
                _patterns.Add(newPattern);
            }
        }
    }

    private void GenerateFourLinePatterns(HashSet<int> lengthsSet)
    {
        var combinationsBy4 = new Multicombination(orders.Length, 4);
        foreach (var comb in combinationsBy4.GetRows())
        {
            lengthsSet.Clear();
            var piece1 = orders[comb[0]];
            var piece2 = orders[comb[1]];
            var piece3 = orders[comb[2]];
            var piece4 = orders[comb[3]];
            var totalPieceWidth = piece1.WorkPieceWidth + piece2.WorkPieceWidth + piece3.WorkPieceWidth +
                                  piece4.WorkPieceWidth;

            lengthsSet.Add(piece1.WorkPieceLength);
            lengthsSet.Add(piece2.WorkPieceLength);
            lengthsSet.Add(piece3.WorkPieceLength);
            lengthsSet.Add(piece4.WorkPieceLength);

            if (piece1.Mark == piece2.Mark &&
                piece2.Mark == piece3.Mark &&
                lengthsSet.Count is 1 or 2 &&
                totalPieceWidth <= Pattern1380.MaxWidth &&
                IsPassedThreshold(Pattern1380.MaxWidth - totalPieceWidth, Pattern1380.MaxWidth))
            {
                var bigLength = lengthsSet.Max();
                var smallLength = lengthsSet.Min();
                var newPattern = new Pattern1380(bigLength, smallLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);
                newPattern.AddOrderIdInProduction(piece3.Id);
                newPattern.AddOrderIdInProduction(piece4.Id);

                newPattern.TimeToShip =
                    (GetMostEarliest(piece1.ShippingDate, piece2.ShippingDate, piece3.ShippingDate, piece4.ShippingDate)
                        .ToDateTime(TimeOnly.MinValue) - DateTime.Today).Duration();
                newPattern.CountLines();
                newPattern.CalculateWaste(totalPieceWidth);
                _patterns.Add(newPattern);
            }
        }
    }

    private static DateOnly GetMostEarliest(params DateOnly[] dates)
    {
        var result = dates[0];
        for (var i = 1; i < dates.Length; i++)
            result = dates[i].CompareTo(result) == -1 ? dates[i] : result;

        return result;
    }

    public void Solve()
    {
        var lengthsSet = new HashSet<int>();
        GenerateOneLinePatterns();
        GenerateTwoLinePatterns(lengthsSet);
        GenerateThreeLinePatterns(lengthsSet);
        GenerateFourLinePatterns(lengthsSet);
    }

    public List<Pattern> GetPatterns() => _patterns;
}