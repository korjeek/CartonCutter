using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class DistributionSolver(List<Pattern> patterns, Order[] orders)
{
    private readonly Dictionary<int, int> ordersLeftAmount = new(); // id заказа и оставшееся количество
    private readonly List<Pattern> resultPatterns = [];

    public Dictionary<int, int> GetLeftOrdersAmount() => ordersLeftAmount
        .Where(kv => kv.Value != 0)
        .ToDictionary();
    public List<Pattern> GetResultPatterns() => resultPatterns;
    public async Task Solve()
    {
        await Task.Run(() =>
        {
            SortPatterns();
            DistributeOrders();
        });
    }

    private void SortPatterns()
    {
        patterns.Sort((p1, p2) =>
        {
            var dateCompare = p1.TimeToShip.CompareTo(p2.TimeToShip);
            return dateCompare != 0 ? dateCompare : p2.TotalLinesCount.CompareTo(p1.TotalLinesCount);
        });
    }

    private void DistributeOrders()
    {
        FillInitialLeftAmountDictionary();
        
        /*
         * Что делаем?
         * 1. Определить, сколько можжно максимум сделать шаблонов, чтобы не выйти за пределы заказа (допустимо немного выйти)
         * Чтобы это сделать, надо Amount каждого заказа раздеить на orderIdCount и округилить вверх - так мы найдём
         * целое количество шаблонов, Которое МОЖНО произвести, учитывая наши остатки
         */
        
        foreach (var pattern in patterns)
        {
            if (pattern is Pattern1030 pattern1030)
            {
                var minOrderAmount = ordersLeftAmount
                    .Where(kv => pattern1030.TryGetOrderCountById(kv.Key, out _))
                    .Select(kv =>
                    {
                        pattern1030.TryGetOrderCountById(kv.Key, out var orderInPatternCount);
                        return kv.Value / orderInPatternCount + (kv.Value % orderInPatternCount == 0 ? 0 : 1);
                    })
                    .Min(); // Нашли допустимое количество шаблонов, которе можно сделать, чтобы не выйти за пределы каждого заказа
                if (minOrderAmount <= 0)
                    continue;
                foreach (var (orderId, orderIdCount) in pattern1030)
                    ordersLeftAmount[orderId] -= minOrderAmount * orderIdCount;

                pattern1030.ProductTimesAmount = minOrderAmount;
                resultPatterns.Add(pattern1030);
            }
            
            /*
             * Если другой шаблон, то:
             * 1. Находим минимальное оставшееся количество коротких заказов, которое нужно сделать
             * 2. Считаем, сколько при этом нужно будет сделать длинных заказов, чтобы длина коротких более менее
             * сошлась по длинным
             * 3. Если такое количество длинных имеется, то делаем
             * 4. Если такого количества долинных нет, то нужно посчитать, сколько всего длинных и посчитать,
             * сколько мы можем сделать коротких при таком раскладе
             */
            if (pattern is Pattern1380 pattern1380)
            {
                var minSmallOrderAmount = ordersLeftAmount
                    .Where(kv => pattern1380.TryGetOrderCountById(kv.Key, out _))
                    .Where(kv =>
                        orders.GetOrderById(kv.Key).WorkPieceLength == pattern1380.SmallLength)
                    .Select(kv =>
                    {
                        pattern1380.TryGetOrderCountById(kv.Key, out var orderInPatternCount);
                        return kv.Value / orderInPatternCount + (kv.Value % orderInPatternCount == 0 ? 0 : 1);
                    })
                    .Min();
                var minBigOrdersAmount = ordersLeftAmount
                    .Where(kv => pattern1380.TryGetOrderCountById(kv.Key, out _))
                    .Where(kv =>
                        orders.GetOrderById(kv.Key).WorkPieceLength == pattern1380.BigLength)
                    .Select(kv =>
                    {
                        pattern1380.TryGetOrderCountById(kv.Key, out var orderInPatternCount);
                        return kv.Value / orderInPatternCount + (kv.Value % orderInPatternCount == 0 ? 0 : 1);
                    })
                    .Min();
                
                if (minSmallOrderAmount <= 0 || minBigOrdersAmount <= 0)
                    continue;

                var smallOrdersTotalLength = minSmallOrderAmount * pattern1380.SmallLength;
                var bigOrdersTotalLength = minBigOrdersAmount * pattern1380.BigLength;
                
                
                var bigOrdersCountedAmount = smallOrdersTotalLength / pattern1380.BigLength;

                if (minBigOrdersAmount >= bigOrdersCountedAmount)
                {
                    minBigOrdersAmount = bigOrdersCountedAmount;
                    var lastDiff = smallOrdersTotalLength - bigOrdersTotalLength;
                    var probDiff = pattern1380.BigLength - lastDiff;
                    if (probDiff < lastDiff)
                        minBigOrdersAmount++;
                }
                else
                {
                    minSmallOrderAmount = bigOrdersTotalLength / pattern1380.SmallLength;
                    smallOrdersTotalLength = minSmallOrderAmount * pattern1380.SmallLength;
                    var lastDiff = bigOrdersTotalLength - smallOrdersTotalLength;
                    var probDiff = pattern1380.SmallLength - lastDiff;
                    if (probDiff < lastDiff)
                        minSmallOrderAmount++;
                }

                if (minSmallOrderAmount <= 0 || minBigOrdersAmount <= 0)
                    continue;

                foreach (var (orderId, orderIdCount) in pattern1380)
                    ordersLeftAmount[orderId] -= 
                        orders.GetOrderById(orderId).WorkPieceLength == pattern1380.BigLength ? 
                            minBigOrdersAmount : minSmallOrderAmount  * orderIdCount;
                
                
                pattern1380.ProductBigTimesAmount = minBigOrdersAmount;
                pattern1380.ProductSmallTimesAmount = minSmallOrderAmount;
                resultPatterns.Add(pattern1380);
            }
        }
    }

    private void FillInitialLeftAmountDictionary()
    {
        foreach (var order in orders)
            ordersLeftAmount.Add(order.Id, order.Amount);
    }
}