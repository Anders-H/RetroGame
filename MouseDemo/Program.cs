using System;
using System.Runtime.Versioning;

namespace MouseDemo;

[SupportedOSPlatform("windows")]
public static class Program
{
    [STAThread]
    private static void Main()
    {
        using var game = new Game1();
        game.Run();
    }
}