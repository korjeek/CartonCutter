using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Algorithm(Order[] orders, int threshold)
{
    private readonly ColumnGenerationSolver _patternGenerator = new(orders, threshold);
    private DistributionSolver _distributionSolver = null!;
    
    public List<Pattern> Solve()
    {
        _patternGenerator.Solve();
        _distributionSolver = new DistributionSolver(_patternGenerator.GetPatterns(), orders);
        _distributionSolver.Solve();
        
        foreach (var pattern in _distributionSolver.GetResultPatterns())
        {
            if (pattern is Pattern1380 pattern1380)
            {
                pattern1380.FillOrdersAmountById(orders);
                continue;
            }
            ((Pattern1030)pattern).FillOrdersAmountById();
        }

        return _distributionSolver.GetResultPatterns();
    }
    
    public Dictionary<int, int> GetLeftAmountsDictByOrderId() => _distributionSolver.GetLeftOrdersAmount();
}