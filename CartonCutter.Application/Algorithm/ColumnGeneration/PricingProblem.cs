using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm.ColumnGeneration;

public class PricingProblem
{
    private readonly Order[] orders;
    private readonly int maxListWidth;

    public PricingProblem(Order[] orders, MachineType machineType = MachineType.Machine1030)
    {
        this.orders = orders;
        maxListWidth = machineType == MachineType.Machine1030 ? 1030 : 1380;
    }

    // Генерация нового шаблона.
    // Здесь используем простую эвристику:
    // Для каждого заказа (как базовой длины) перебираем, сколько единиц можно разместить на листе,
    // затем вычисляем редуцированную стоимость.
    public Pattern? GenerateNewPattern(Dictionary<int, double> duals)
    {
        Pattern? bestPattern = null;
        var bestReducedCost = 1e-6;

        // Перебираем заказы, чтобы попытаться сгенерировать шаблон для каждой фиксированной длины.
        foreach (var order in orders)
        {
            // Все заготовки в шаблоне должны иметь длину order.Length.
            // Определяем максимальное число заготовок данного заказа, которые могут уместиться по ширине листа.
            var maxPieces = maxListWidth / order.WorkPieceWidth;
            
            // Перебираем варианты: от 1 до maxPieces.
            for (var pieces = 1; pieces <= maxPieces; pieces++)
            {
                // Создаем новый шаблон для заказа с фиксированной длиной.
                var pattern = new Pattern(order.WorkPieceLength);
                pattern.Production[order.Id] = pieces;
                pattern.CalculateWaste(CreateOrdersDict(orders));
                // TODO: убрать метод создания словаря, пусть и так передаётся словарь

                // Редуцированная стоимость шаблона:
                // reducedCost = waste - dual_value * количество произведенных единиц
                var reducedCost = pattern.Waste;
                if (duals.TryGetValue(order.Id, out var dualVal))
                    reducedCost -= dualVal * pieces;
                
                
                // Если редуцированная стоимость отрицательная и лучше предыдущего,
                // считаем этот шаблон как кандидата.
                if (reducedCost < bestReducedCost)
                {
                    bestReducedCost = reducedCost;
                    bestPattern = pattern;
                }
            }
        }

        return bestPattern;
    }


    private static Dictionary<int, Order> CreateOrdersDict(Order[] ordersToDict) => ordersToDict.ToDictionary(o => o.Id);
}