using CartonCutter.Domain.Models;
using Google.OrTools.LinearSolver;

namespace CartonCutter.Application.Algorithm.ColumnGeneration;

public class MasterProblem
    {
        private readonly Order[] orders;
        // Словарь для быстрого доступа по id заказа.
        private Dictionary<int, Order> OrdersDict { get; }
        // Список шаблонов (колонок)
        public List<Pattern> Patterns { get; set; }
        // Словарь переменных для каждого шаблона: Pattern → переменная x_p
        private Dictionary<Pattern, Variable> PatternVars { get; }
        // private Solver Solver { get; }

        public MasterProblem(Order[] orders)
        {
            this.orders = orders;
            OrdersDict = orders.ToDictionary(o => o.Id);
            Patterns = new List<Pattern>();
            PatternVars = new Dictionary<Pattern, Variable>();
            // Solver = Solver.CreateSolver("GLOP"); // CBC_MIXED_INTEGER_PROGRAMMING
            // if (Solver == null)
            //     throw new Exception("Ошибка: не удалось создать решатель.");
        }

        
        public void GenerateInitialPatterns()
        {
            foreach (var order in orders)
            {
                var pattern = new Pattern(order.WorkPieceLength);
                pattern.Production[order.Id] = 1;
                pattern.CalculateWaste(OrdersDict);
                Patterns.Add(pattern);
            }
        }

        
        public double SolveMaster(out Dictionary<int, double> duals)
        {
            PatternVars.Clear();
            
            
            var solver = Solver.CreateSolver("GLOP"); // CBC_MIXED_INTEGER_PROGRAMMING
            if (solver == null)
                throw new Exception("Ошибка: не удалось создать решатель.");
            
            
            foreach (var pattern in Patterns)
            {
                var x = solver.MakeNumVar(0.0, double.PositiveInfinity, $"x_p_{Patterns.IndexOf(pattern)}");
                PatternVars[pattern] = x;
            }

            // Ограничения: для каждого заказа суммарное производство должно быть не меньше требуемого количества.
            foreach (var order in orders)
            {
                var constr = solver.MakeConstraint(order.Amount, double.PositiveInfinity, $"order_{order.Id}");
                foreach (var pattern in Patterns)
                {
                    if (pattern.Production.TryGetValue(order.Id, out var orderInPatternCount))
                        constr.SetCoefficient(PatternVars[pattern], orderInPatternCount);
                }
            }
            
            var objective = solver.Objective();
            foreach (var pattern in Patterns)
                objective.SetCoefficient(PatternVars[pattern], pattern.Waste);
            objective.SetMinimization();
            
            solver.Solve();

            
            duals = new Dictionary<int, double>();
            foreach (var order in orders)
            {
                var c = solver.LookupConstraintOrNull($"order_{order.Id}");
                duals[order.Id] = c.DualValue();
            }

            return objective.Value();
        }
    }