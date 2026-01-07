using System;

namespace ConsoleApp1.Models;

public class SphereInfo
{
    public IEnumerable<IEnumerable<BlockInfo>> Blocks { get; set; } 
    public int CurrentLayer { get; set; }
    public int CurrentBlock { get; set; }
}
