using System;
using ConsoleApp1.Models;

namespace ConsoleApp1.Infrastructure;

public class TriangleMath(int length)
{
    public int Radius { get; private set; } = length;

    public static double GetAngleRad(int angleDeg)
    {
        return angleDeg * Math.PI / 180.0;
    }

    public TriangleInfo CalculateByDegrees(int angleDeg)
    {
        var angleRad = GetAngleRad(angleDeg);

        var x = Radius * Math.Cos(angleRad);
        var y = Radius * Math.Sin(angleRad);

        return new TriangleInfo()
        {
            Angle = angleDeg,
            X = x,
            Y = y
        };
    }

    public TriangleInfo CalculateByX(int x)
    {
        var y = Math.Sqrt(Radius * Radius - x * x);

        return new TriangleInfo()
        {
            X = x,
            Y = y
        };
    }

    public TriangleInfo CalculateByY(int y)
    {
        var x = Math.Sqrt(Radius * Radius - y * y);

        return new TriangleInfo()
        {
            X = x,
            Y = y
        };
    }

    public void SetRadius(int length)
    {
        Radius = length;
    }
}
