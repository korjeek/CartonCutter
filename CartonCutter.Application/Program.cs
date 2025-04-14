using Google.OrTools.LinearSolver;

namespace CartonCutter.Application;

public class Program
{
    public static void Main()
    {
        var solver = Solver.CreateSolver("SCIP");

        var x = solver.MakeNumVar(0.0, double.MaxValue, "x");
        var y = solver.MakeNumVar(0.0, double.MaxValue, "y");

        Console.WriteLine(solver.Variable(0).Name());

        solver.Add(x + 2 * y <= 14);
        solver.Add(3 * x - y >= 0);
        solver.Add(x - y <= 2);

        Console.WriteLine($"Constraint number: {solver.constraints().Count}");

        solver.Maximize(3 * x + 2 * y);


        var resultStatus = solver.Solve();

        if (resultStatus != Solver.ResultStatus.OPTIMAL)
            Console.WriteLine("No solution");

        Console.WriteLine(solver.Objective().Value());
        Console.WriteLine(x.SolutionValue());
        Console.WriteLine(y.SolutionValue());
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