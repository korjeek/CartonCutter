using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Pattern
{
    public Dictionary<int, int> Production { get; set; } // id Заготовки её количество
    public int PatternLength1 { get; }
    public int PatternLength2 { get; }
    public double Waste { get; set; }

    public MachineType MachineType;
    public readonly int MaxWidth;

    public Pattern(int patternLength1, int patternLength2 = 0, MachineType machineType = MachineType.Machine1030)
    {
        PatternLength1 = patternLength1;
        Production = new Dictionary<int, int>();
        MachineType = machineType;
        PatternLength2 = machineType == MachineType.Machine1030 ? 0 : patternLength2;
        MaxWidth = machineType == MachineType.Machine1030 ? 1030 : 1380;
    }
    
    public void CalculateWaste(Dictionary<int, Order> ordersDict)
    {
        var totalWidth = Production.Sum(kv => kv.Value * ordersDict[kv.Key].WorkPieceWidth);
        Waste = MaxWidth - totalWidth;
    }

    public override string ToString()
    {
        var prodStr = string.Join(", ", Production.Select(kv => $"Order {kv.Key}: {kv.Value}"));
        return $"Pattern (Length: {PatternLength1}) -> [{prodStr}], Waste = {Waste}";
    }
}