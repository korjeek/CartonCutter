using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Algorithm(Order[] orders, int threshold)
{
    private readonly ColumnGenerationSolver _patternGenerator = new(orders, threshold);
    private DistributionSolver _distributionSolver = null!;
    
    public async Task<List<Pattern>> Solve()
    {
        await patternGenerator.Solve();
        // Console.WriteLine(patternGenerator.GetPatterns().Count);
        // foreach (var pattern in patternGenerator.GetPatterns())
        //     Console.WriteLine(pattern);
        
        // Console.WriteLine();
        distributionSolver = new DistributionSolver(patternGenerator.GetPatterns(), orders);
        await distributionSolver.Solve();
        // Console.WriteLine(distributionSolver.GetResultPatterns().Count);
        // foreach (var pattern in distributionSolver.GetResultPatterns()) 
        //     Console.WriteLine(pattern);
        
        // Console.WriteLine("\nLeft");
        // foreach (var p in distributionSolver.GetLeftOrdersAmount())
        //     Console.WriteLine(p);

        await Task.Run(() =>
        {
            foreach (var pattern in distributionSolver.GetResultPatterns())
            {
                if (pattern is Pattern1380 pattern1380)
                {
                    pattern1380.FillOrdersAmountById(orders);
                    continue;
                }

                ((Pattern1030)pattern).FillOrdersAmountById();
            }
        });

        return distributionSolver.GetResultPatterns();
    }
    
    public Dictionary<int, int> GetLeftAmountsDictByOrderId() => distributionSolver.GetLeftOrdersAmount();
}