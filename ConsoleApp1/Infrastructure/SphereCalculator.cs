using System;
using ConsoleApp1.Models;

namespace ConsoleApp1.Infrastructure;

public class SphereCalculator
{
    public SphereCalculator(int radius)
    {
        Radius = radius;
        CircleCalculator = new CircleCalculator(radius);
    }

    public int Radius { get; }
    public CircleCalculator CircleCalculator { get; }

    public SphereInfo Calculate()
    {
        var result = new List<IEnumerable<BlockInfo>>();

        for (int i = 0; i <= Radius; i++)
        {
            var info = CircleCalculator.BlockCircleProvider.GetBlockByY(i);
            var layer = GetLayer(info.X);
            result.Add(layer);
        }

        return new SphereInfo()
        {
            Blocks = result
        };
    }

    protected IEnumerable<BlockInfo> GetLayer(int radius)
    {
        this.CircleCalculator.BlockCircleProvider.Triangle.SetRadius(radius);
        var blocksByX = CircleCalculator.CalculateByX(0, radius);
        var blocksByY = CircleCalculator.CalculateByY(0, radius);
        var middle = radius / 2;
        var blocks = blocksByX.Where(x => x.X <= middle).Concat(blocksByY.Where(x => x.X >= middle)).ToArray();

        return blocks.OrderByDescending(x => x.Y).ThenBy(x => x.X).ToArray();
    }

    public BlockInfo? GetNext(SphereInfo info)
    {
        return null;
    }
}
