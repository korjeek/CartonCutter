using CartonCutter.Application.Algorithm;
using CartonCutter.Domain.Models;

namespace CartonCutter.Application;

public class Program
{
    public static void Main()
    {
        // var orders = new Order[]
        // {
        //     new Order(1, "", "", "", "", null, null, null,
        //         new DateOnly(2025, 1, 23), 872, 612, null, 1000),
        //     new Order(2, "", "", "", "", null, null, null,
        //         new DateOnly(2025, 1, 23), 360, 430, null, 5000),
        //     new Order(3, "", "", "", "", null, null, null,
        //         new DateOnly(2025, 1, 23), 710, 270, null, 2000),
        //     new Order(4, "", "", "", "", null, null, null,
        //         new DateOnly(2025, 1, 23), 1050, 1020, null, 1300),
        //     new Order(5, "", "", "", "", null, null, null,
        //         new DateOnly(2025, 4, 20), 330, 596, null, 300),
        //     new Order(6, "", "", "", "", null, null, null, 
        //         new DateOnly(2025, 1, 23), 1390, 422, null, 3000),
        //     new Order(7, "", "", "", "", null, null, null, 
        //         new DateOnly(2025, 1, 23), 2067, 598, null, 2700)
        // };
        //
        // var patternGenerator = new ColumnGenerationSolver(orders, 19);
        // patternGenerator.Solve();
        // var patterns = patternGenerator.GetPatterns();
        // Console.WriteLine(patterns.Count);
        // Console.WriteLine(string.Join("\n", patterns));
        //
        //
        // Console.WriteLine();
        //
        // var distributionSolver = new DistributionSolver(patterns, orders);
        // distributionSolver.Solve();
        // var left = distributionSolver.GetLeftOrdersAmount();
        //
        // Console.WriteLine(distributionSolver.GetResultPatterns().Count);
        // Console.WriteLine($"Result\n{string.Join("\n", distributionSolver.GetResultPatterns())}");
        // Console.WriteLine($"Left\n{string.Join("\n", left)}");
        
        // var solver = Solver.CreateSolver("SCIP");
        //
        // var x = solver.MakeNumVar(0.0, double.MaxValue, "x");
        // var y = solver.MakeNumVar(0.0, double.MaxValue, "y");
        //
        // // Console.WriteLine(solver.Variable(0).Name());
        //
        // solver.Add(x + 2 * y <= 14);
        // solver.Add(3 * x - y >= 0);
        // solver.Add(x - y <= 2);
        //
        // // Console.WriteLine($"Constraint number: {solver.constraints().Count}");
        //
        // solver.Maximize(3 * x + 2 * y);
        //
        //
        // var resultStatus = solver.Solve();
        //
        // if (resultStatus != Solver.ResultStatus.OPTIMAL)
        //     Console.WriteLine("No solution");
        //
        // Console.WriteLine(solver.Objective().Value());
        // Console.WriteLine(x.SolutionValue());
        // Console.WriteLine(y.SolutionValue());
    }
}