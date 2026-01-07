using System;
using ConsoleApp1.Models;

namespace ConsoleApp1.Infrastructure;

public class BlockCircleProvider(int length)
{
    public TriangleMath Triangle { get; } = new TriangleMath(length);
    public BlockInfo GetBlockByAngleInDegrees(int angle)
    {
        var info = Triangle.CalculateByDegrees(angle);

        return new BlockInfo()
        {
            X = (int)Math.Ceiling(info.X),
            Y = (int)Math.Ceiling(info.Y)
        };
    }

    public BlockInfo GetBlockByX(int x)
    {
        var info = Triangle.CalculateByX(x);

        return new BlockInfo()
        {
            X = (int)Math.Ceiling(info.X),
            Y = (int)Math.Ceiling(info.Y)
        };
    }

    public BlockInfo GetBlockByY(int y)
    {
        var info = Triangle.CalculateByY(y);

        return new BlockInfo()
        {
            X = (int)Math.Ceiling(info.X),
            Y = (int)Math.Ceiling(info.Y)
        };
    }
}
