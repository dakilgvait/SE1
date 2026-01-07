// See https://aka.ms/new-console-template for more information
using System.Xml.Schema;
using ConsoleApp1.Infrastructure;
using ConsoleApp1.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var radius = 95;
        var sphere = new SphereCalculator(radius);
        var layers = sphere.Calculate();



        var border = new string('*', radius);
        Console.WriteLine(border);
        BlockInfo? previousBlock = null;
        foreach (var block in layers.Blocks.Skip(0).First())
        {
            if (previousBlock is not null)
            {
                var emptyLine = new string(' ', block.X);
                if (previousBlock.Y > block.Y)
                {
                    Console.Write($"{Environment.NewLine}{emptyLine}");
                }
            }
            Console.Write("#");
            previousBlock = block;
        }
        Console.Write(Environment.NewLine);
        Console.WriteLine(border);
    }
}