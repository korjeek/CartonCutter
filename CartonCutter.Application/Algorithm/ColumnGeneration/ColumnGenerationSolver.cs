using CartonCutter.Domain.Models;
using NPOI.XWPF.UserModel;

namespace CartonCutter.Application.Algorithm.ColumnGeneration;

 // Основной цикл колоночной генерации для одного станка.
    public class ColumnGenerationSolver
    {
        private readonly MasterProblem master;
        private readonly PricingProblem pricing;
        private const int MaxIterations = 50;

        public ColumnGenerationSolver(Order[] orders)
        {
            master = new MasterProblem(orders);
            pricing = new PricingProblem(orders);
        }

        public void Solve()
        {
            // Шаг 1: Генерация начальных шаблонов
            master.GenerateInitialPatterns();

            var iteration = 0;
            var newPatternFound = true;

            // Итеративно решаем мастер-задачу и запускаем задачу ценообразования.
            while (newPatternFound && iteration < MaxIterations)
            {
                iteration++;
                Console.WriteLine($"\nИтерация {iteration}: решаем мастер-задачу");
                var objValue = master.SolveMaster(out var duals);
                Console.WriteLine($"Значение цели (суммарные отходы) = {objValue:F2}");
                Console.WriteLine("Двойственные значения:");
                foreach (var kv in duals)
                    Console.WriteLine($"Order {kv.Key}: {kv.Value:F2}");

                // Шаг 3: Решаем задачу ценообразования, чтобы найти новый шаблон.
                var newPattern = pricing.GenerateNewPattern(duals);
                if (newPattern != null)
                {
                    Console.WriteLine($"Найден новый шаблон: {newPattern}");
                    master.Patterns.Add(newPattern);
                }
                else
                {
                    Console.WriteLine("Новых шаблонов не найдено. Останавливаем итерации.");
                    newPatternFound = false;
                }
            }

            Console.WriteLine("\nЗакончилась процедура колоночной генерации.");
        }

        public List<Pattern> GetPatterns() => master.Patterns;
    }