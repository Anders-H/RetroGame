using System;
using System.Runtime.Versioning;

namespace TilesDemo;

public static class Program
{
    [STAThread]
    [SupportedOSPlatform("windows")]
    private static void Main()
    {
        using var game = new Game1();
        game.Run();
    }
}