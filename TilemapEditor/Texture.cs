namespace TilemapEditor;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class Texture
{
    public Bitmap Bitmap { get;}
    public int TileSizeX { get; }
    public int TileSizeY { get; }
    public int Count { get; }

    public Texture(int tileSizeX, int tileSizeY)
    {
        TileSizeX = tileSizeX;
        TileSizeY = tileSizeY;
        Bitmap = new Bitmap(TileSizeX * 3, TileSizeY);
        var colors = new[] { Brushes.Red, Brushes.Black, Brushes.Blue };

        using (var g = Graphics.FromImage(Bitmap))
        {
            for (var x = 0; x < 3; x++)
                g.FillRectangle(colors[x], x * TileSizeX, 0, TileSizeX - 1, TileSizeY - 1);
        }

        Count = 3;
    }

    public Texture(int tileSizeX, int tileSizeY, Bitmap bitmap)
    {
        TileSizeX = tileSizeX;
        TileSizeY = tileSizeY;
        Bitmap = bitmap;
    }
}