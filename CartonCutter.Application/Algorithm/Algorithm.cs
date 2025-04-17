using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Algorithm
{
    private readonly Order[] orders;
    private readonly ColumnGenerationSolver patternGenerator;

    public Algorithm(Order[] orders, int threshold)
    {
        this.orders = orders;
        patternGenerator = new ColumnGenerationSolver(orders, threshold);
    }

    public void Solve()
    {
        patternGenerator.Solve();
        var patterns = patternGenerator.GetPatterns();
        var cplexSolver = new CplexSolver(patterns, orders);
        cplexSolver.Solve();



        // Console.WriteLine(patterns.Count);
        // foreach (var pattern in patterns)
        //     Console.WriteLine(pattern);
    }
}