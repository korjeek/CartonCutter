using CartonCutter.Domain.Models;
using Google.OrTools.LinearSolver;

namespace CartonCutter.Application.Algorithm;

public class CplexSolver
{
    private Order[] Orders { get; }
    private Dictionary<int, Constraint> OrdersConstraints { get; }
    private List<Pattern1030> Patterns { get; }
    private Variable[] PatternVars { get; } // Соответствие между индексами в Patterns и переменными

    public CplexSolver(List<Pattern1030> patterns, Order[] orders)
    {
        Patterns = patterns;
        PatternVars = new Variable[patterns.Count];
        Orders = orders;
        OrdersConstraints = new Dictionary<int, Constraint>();
    }


    public double Solve()
    {
        Array.Clear(PatternVars);

        var solver = Solver.CreateSolver("GLOP"); // CBC_MIXED_INTEGER_PROGRAMMING
        if (solver == null)
            throw new Exception("Ошибка: не удалось создать решатель.");

        for (var i = 0; i < Patterns.Count; i++)
        {
            var x = solver.MakeNumVar(0.0, double.PositiveInfinity, $"x_p_{i}");
            PatternVars[i] = x;
        }


        for (var i = 0; i < Patterns.Count; i++)
        {
            var currentPattern = Patterns[i];
            var minOrderIdAmountInPattern = currentPattern.MinBy(p => Orders[p.orderId - 1].Amount).orderId;
            currentPattern.GetOrderCountById(minOrderIdAmountInPattern, out var orderInPatternCount);

            if (!OrdersConstraints.TryGetValue(minOrderIdAmountInPattern, out var constr))
            {
                var constraint = solver.MakeConstraint(Orders[minOrderIdAmountInPattern - 1].Amount,
                    double.PositiveInfinity, $"order_{minOrderIdAmountInPattern}");
                OrdersConstraints.Add(minOrderIdAmountInPattern, constraint);
                constraint.SetCoefficient(PatternVars[i], orderInPatternCount);
            }
            else
                constr.SetCoefficient(PatternVars[i], orderInPatternCount);
        }


        // // Ограничения: для каждого заказа суммарное производство должно быть не меньше требуемого количества.
        // foreach (var order in Orders)
        // {
        //     var constr = solver.MakeConstraint(order.Amount, double.PositiveInfinity, $"order_{order.Id}");
        //     for (var i = 0; i < Patterns.Count; i++)
        //     {
        //         if (Patterns[i].GetOrderCountById(order.Id, out var orderInPatternCount))
        //             constr.SetCoefficient(PatternVars[i], orderInPatternCount);
        //     }
        // }


        var objective = solver.Objective();
        for (var i = 0; i < Patterns.Count; i++)
            objective.SetCoefficient(PatternVars[i], Patterns[i].Waste);
        objective.SetMinimization();

        var resultStatus = solver.Solve();

        Console.WriteLine(resultStatus == Solver.ResultStatus.OPTIMAL);
        for (var i = 0; i < Patterns.Count; i++)
        {
            Console.WriteLine($"{Patterns[i]}, resultAmount = {PatternVars[i].SolutionValue()}");
        }

        // duals = new Dictionary<int, double>();
        // foreach (var order in orders) 
        // {
        //     var c = solver.LookupConstraintOrNull($"order_{order.Id}");
        //     duals[order.Id] = c.DualValue();
        // }

        return objective.Value();
    }
}