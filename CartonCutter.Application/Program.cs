using CartonCutter.Application.Algorithm;
using CartonCutter.Domain.Models;

namespace CartonCutter.Application;

public class Program
{
    public static void Main()
    {
        var orders = new Order[]
        {
            new(1, "", "", "", "", null, null, null,
                new DateOnly(2025, 1, 23), 872, 612, null, 1000),
            new(2, "", "", "", "", null, null, null,
                new DateOnly(2025, 1, 23), 360, 430, null, 5000),
            new(3, "", "", "", "", null, null, null,
                new DateOnly(2025, 1, 23), 710, 270, null, 2000),
            new(4, "", "", "", "", null, null, null,
                new DateOnly(2025, 1, 23), 1050, 1020, null, 1300),
            new(5, "", "", "", "", null, null, null,
                new DateOnly(2025, 4, 20), 330, 596, null, 300),
            new(6, "", "", "", "", null, null, null, 
                new DateOnly(2025, 1, 23), 1390, 422, null, 3000),
            new(7, "", "", "", "", null, null, null, 
                new DateOnly(2025, 1, 23), 2067, 598, null, 2700)
        };

        var patternGenerator = new ColumnGenerationSolver(orders, 19);
        patternGenerator.Solve();
        var patterns = patternGenerator.GetPatterns();
        Console.WriteLine(patterns.Count);
        Console.WriteLine(string.Join("\n", patterns));
        
        
        Console.WriteLine();
        
        var distributionSolver = new DistributionSolver(patterns, orders);
        distributionSolver.Solve();
        var left = distributionSolver.GetLeftOrdersAmount();
        
        Console.WriteLine(distributionSolver.GetResultPatterns().Count);
        Console.WriteLine($"Result\n{string.Join("\n", distributionSolver.GetResultPatterns())}");
        Console.WriteLine($"Left\n{string.Join("\n", left)}");

        // var d1 = new DateOnly(2004, 12, 11);
        // var d2 = new DateOnly(2000, 11, 1);
        // var d3 = d2.ToDateTime(TimeOnly.MinValue) - d1.ToDateTime(TimeOnly.MinValue);
        // var r = d1.CompareTo(d2);
        // Console.WriteLine(r);
        // Console.WriteLine($"{d3.Days} {d3.Hours}");
        // Console.WriteLine($"{d3}");
        // Console.WriteLine(new TimeSpan(12, 12, 12, 12) > d3);
        // Console.WriteLine((DateTime.Today - d1.ToDateTime(TimeOnly.MinValue)).Days );


        // var combinations = new Multicombination(5, 3);
        // Console.WriteLine(combinations.RowCount);
        // foreach (var combination in combinations.GetRows())
        // {
        //     Console.WriteLine(combination);
        //     // Console.WriteLine(combination[0]);
        //     // Console.WriteLine(combination[1]);
        //     // Console.WriteLine(combination[2]);
        //     // Console.WriteLine();
        // }

        // var generator = new ColumnGenerationSolver(orders.ToList(), 15);
        // generator.StupidSolve();
        //
        // Console.WriteLine(generator.GetPatterns().Count);
        // foreach (var pattern in generator.GetPatterns())
        //     Console.WriteLine(pattern);



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

// class Program1
// {
//     static void Main1(string[] args)
//     {
//         // Пример входных данных: список заказов
//         List<Order> orders = new List<Order>
//         {
//             new Order(1, 150, 500, 300, DateTime.Now.AddDays(5)),
//             new Order(2, 100, 600, 250, DateTime.Now.AddDays(3)),
//             new Order(3, 200, 400, 200, DateTime.Now.AddDays(7)),
//             new Order(4, 120, 550, 320, DateTime.Now.AddDays(4))
//             // можно добавить больше заказов
//         };
//
//         // Решение для станка 2 (machineId = 2)
//         ColumnGenerationSolver solver = new ColumnGenerationSolver(orders, machineId: 2);
//         solver.Solve();
//
//         Console.WriteLine("\nНажмите любую клавишу для завершения...");
//         Console.ReadKey();
//     }
// }