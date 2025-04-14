using CartonCutter.Domain.Models;
using Google.OrTools.LinearSolver;

namespace CartonCutter.Application.Algorithm;

public class MasterProblem
    {
        private readonly List<Order> orders;
        // Словарь для быстрого доступа по id заказа.
        public Dictionary<int, Order> OrdersDict { get; }
        // Список шаблонов (колонок)
        public List<Pattern> Patterns { get; set; }
        // Словарь переменных для каждого шаблона: Pattern → переменная x_p
        public Dictionary<Pattern, Variable> PatternVars { get; }
        public Solver Solver { get; }

        public MasterProblem(List<Order> orders)
        {
            this.orders = orders;
            OrdersDict = orders.ToDictionary(o => o.Id);
            Patterns = new List<Pattern>();
            PatternVars = new Dictionary<Pattern, Variable>();
            Solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");
            if (Solver == null)
                throw new Exception("Ошибка: не удалось создать решатель.");
        }

        // Генерирует начальные шаблоны: для каждого заказа создаём шаблон только с этим заказом.
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
            Solver.Reset();
            
            foreach (var pattern in Patterns)
            {
                var x = Solver.MakeNumVar(0.0, double.PositiveInfinity, $"x_p_{Patterns.IndexOf(pattern)}");
                PatternVars[pattern] = x;
            }

            // Ограничения: для каждого заказа суммарное производство должно быть не меньше требуемого количества.
            foreach (var order in orders)
            {
                var constr = Solver.MakeConstraint(order.Amount, double.PositiveInfinity, $"order_{order.Id}");
                foreach (var pattern in Patterns)
                {
                    // Если шаблон произведет заказ с данным id, добавляем коэффициент производства.
                    if (pattern.Production.ContainsKey(order.Id))
                    {
                        constr.SetCoefficient(PatternVars[pattern], pattern.Production[order.Id]);
                    }
                }
            }

            // Целевая функция: минимизация суммарных отходов по шаблонам.
            Objective objective = Solver.Objective();
            foreach (var pattern in Patterns)
            {
                objective.SetCoefficient(PatternVars[pattern], pattern.Waste);
            }
            objective.SetMinimization();

            // Решаем LP
            Solver.Solve();

            // Извлекаем двойственные значения для ограничений по заказам.
            duals = new Dictionary<int, double>();
            foreach (var order in orders)
            {
                Constraint c = Solver.LookupConstraint($"order_{order.Id}");
                duals[order.Id] = c.DualValue();
            }

            return objective.Value();
        }
    }