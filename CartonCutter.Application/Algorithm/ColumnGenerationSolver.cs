using CartonCutter.Domain.Models;
using Kaos.Combinatorics;

namespace CartonCutter.Application.Algorithm;

    public class ColumnGenerationSolver
    {
        private readonly Order[] orders;
        private readonly List<Pattern1030> patterns;
        private readonly int threshold;
        
        public ColumnGenerationSolver(Order[] orders, int threshold)
        {
            this.orders = orders;
            this.threshold = threshold;
            patterns = new List<Pattern1030>();
        }

        private bool IsPassedThreshold(double waste) => waste / Pattern1030.MaxWidth * 100 <= threshold; 

        public void Solve()
        {
            // для одной полоски
            foreach (var piece in orders)
            {
                if (piece.WorkPieceWidth > Pattern1030.MaxWidth ||
                    !IsPassedThreshold(Pattern1030.MaxWidth - piece.WorkPieceWidth)) continue;
                
                var newPattern = new Pattern1030(piece.WorkPieceLength);
                newPattern.AddOrderIdInProduction(piece.Id); 
                newPattern.CalculateWaste(piece.WorkPieceWidth);
                patterns.Add(newPattern);
            }
            
            
            // для двух полосок
            var combinationsBy2 = new Multicombination(orders.Length, 2);
            foreach (var comb in combinationsBy2.GetRows())
            {
                var piece1 = orders[comb[0]];
                var piece2 = orders[comb[1]];
                var totalPieceWidth = piece1.WorkPieceWidth + piece2.WorkPieceWidth;
                
                
                if (piece1.Mark != piece2.Mark ||
                    piece1.WorkPieceLength != piece2.WorkPieceLength || 
                    totalPieceWidth > Pattern1030.MaxWidth ||
                    !IsPassedThreshold(Pattern1030.MaxWidth - totalPieceWidth)) continue;
                
                var newPattern = new Pattern1030(piece1.WorkPieceLength);
                
                
                
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);
                newPattern.CalculateWaste(totalPieceWidth);
                patterns.Add(newPattern);
            }
            
            
            // для трёх полосок
            var combinationsBy3 = new Multicombination(orders.Length, 3);
            foreach (var comb in combinationsBy3.GetRows())
            {
                var piece1 = orders[comb[0]];
                var piece2 = orders[comb[1]];
                var piece3 = orders[comb[2]];
                var totalPieceWidth = piece1.WorkPieceWidth + piece2.WorkPieceWidth + piece3.WorkPieceWidth;

                if (piece1.Mark != piece2.Mark ||
                    piece2.Mark != piece3.Mark ||
                    piece1.WorkPieceLength != piece2.WorkPieceLength ||
                    piece2.WorkPieceLength != piece3.WorkPieceLength ||
                    totalPieceWidth > Pattern1030.MaxWidth ||
                    !IsPassedThreshold(Pattern1030.MaxWidth - totalPieceWidth))
                    continue;


                var newPattern = new Pattern1030(piece1.WorkPieceLength);
                newPattern.AddOrderIdInProduction(piece1.Id);
                newPattern.AddOrderIdInProduction(piece2.Id);
                newPattern.AddOrderIdInProduction(piece3.Id);
                newPattern.CalculateWaste(totalPieceWidth);
                patterns.Add(newPattern);
            }
        }
        
        public List<Pattern1030> GetPatterns() => patterns;
    }