using System;
using ConsoleApp1.Models;

namespace ConsoleApp1.Infrastructure;

public class CircleCalculator(int length)
{
    public BlockCircleProvider BlockCircleProvider { get; } = new BlockCircleProvider(length);

    public IEnumerable<BlockInfo> CalculateByDegrees(int fromAngleDeg, int toAngleDeg)
    {
        var list = new List<BlockInfo>(toAngleDeg - fromAngleDeg);
        for (var i = fromAngleDeg; i <= toAngleDeg; i++)
        {
            var block = BlockCircleProvider.GetBlockByAngleInDegrees(i);
            list.Add(block);
        }

        return list;
    }

    public IEnumerable<BlockInfo> CalculateByX(int from, int to)
    {
        var list = new List<BlockInfo>(to - from);
        for (var i = from; i <= to; i++)
        {
            var block = BlockCircleProvider.GetBlockByX(i);
            list.Add(block);
        }

        return list;
    }

    public IEnumerable<BlockInfo> CalculateByY(int from, int to)
    {
        var list = new List<BlockInfo>(to - from);
        for (var i = from; i <= to; i++)
        {
            var block = BlockCircleProvider.GetBlockByY(i);
            list.Add(block);
        }

        return list;
    }
}
