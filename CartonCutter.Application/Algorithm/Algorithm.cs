using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Algorithm(Order[] orders, int threshold)
{
    private readonly ColumnGenerationSolver patternGenerator = new(orders, threshold);
    private DistributionSolver distributionSolver = null!;


    /*
     * Этапы сортировки шаблонов:
     * 1. Сортируем по дате (потоиму что нужно закончить ближе всех те заказы, )
     * 2. Сортируем по количеству заготовок в шаблоне (потому что, чем больше сделаем за один раз, тем лучше)
     * Получаем порядок шаблонов, по которому мы можем пройтись
     */

    public List<Pattern> Solve()
    {
        patternGenerator.Solve();
        // var patterns = patternGenerator.GetPatterns();
        // Console.WriteLine(patterns.Count);
        // foreach (var pattern in patterns)
        //     Console.WriteLine(pattern);
        
        
        distributionSolver = new DistributionSolver(patternGenerator.GetPatterns(), orders);
        distributionSolver.Solve();
        
        
        
        // var left = distributionSolver.GetLeftOrdersAmount();
        //
        // Console.WriteLine(distributionSolver.GetResultPatterns().Count);
        // foreach (var pattern in distributionSolver.GetResultPatterns())
        //     Console.WriteLine(pattern);
        // foreach (var p in left)
        //     Console.WriteLine(p);
        foreach (var pattern in distributionSolver.GetResultPatterns())
        {
            if (pattern is Pattern1380 pattern1380)
            {
                pattern1380.FillOrdersAmountById(orders);
                continue;
            }
            ((Pattern1030)pattern).FillOrdersAmountById();
        }

        return distributionSolver.GetResultPatterns();
    }
    
    public Dictionary<int, int> GetLeftAmountsDictByOrderId() => distributionSolver.GetLeftOrdersAmount();
}