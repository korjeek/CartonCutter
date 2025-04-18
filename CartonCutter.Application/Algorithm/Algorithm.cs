using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Algorithm
{
    private readonly Order[] orders;
    private readonly ColumnGenerationSolver patternGenerator;
    private DistributionSolver distributionSolver;

    
    /*
     * Этапы сортировки шаблонов:
     * 1. Сортируем по дате (потоиму что нужно закончить ближе всех те заказы, )
     * 2. Сортируем по количеству заготовок в шаблоне (потому что, чем больше сделаем за один раз, тем лучше)
     * Получаем порядок шаблонов, по которому мы можем пройтись
     */
    
    public Algorithm(Order[] orders, int threshold)
    {
        this.orders = orders;
        patternGenerator = new ColumnGenerationSolver(orders, threshold);
        
    }

    public void Solve()
    {
        patternGenerator.Solve();
        var patterns = patternGenerator.GetPatterns();
        distributionSolver = new DistributionSolver(patterns, orders);
        distributionSolver.Solve();
        
        Console.WriteLine(patterns.Count);
        Console.WriteLine(distributionSolver.GetResultPatterns().Count);
        // foreach (var pattern in patterns)
        //     Console.WriteLine(pattern);
    }
}