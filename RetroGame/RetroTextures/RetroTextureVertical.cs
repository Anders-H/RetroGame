using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RetroGame.RetroTextures;

public class RetroTextureVertical : Texture2D
{
    public int CellWidth => Width;
    public int CellHeight { get; }
    public int CellCount { get; }

    public RetroTextureVertical(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : this(graphicsDevice, cellSize.X, cellSize.Y, cellCount)
    {
    }

    public RetroTextureVertical(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth, cellHeight * cellCount)
    {
        CellHeight = cellHeight;
        CellCount = cellCount;
    }

    public static RetroTextureVertical ScaffoldSimpleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) =>
        ScaffoldTextureCells(graphicsDevice, width, height, 1, color);

    public static RetroTextureVertical ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color)
    {
        var size = cellWidth * cellCount * cellHeight;
        var pixels = new Color[size];

        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        var texture = new RetroTextureVertical(graphicsDevice, cellWidth, cellHeight, cellCount);
        texture.SetData(pixels);
        return texture;
    }

    public void PlotCell(RetroTextureVertical targetTexture, int cellIndex, ColorPalette background, string pixelData)
    {
        var bitmap = new Bitmap(CellWidth, CellHeight, background);
        bitmap.SetPixels(pixelData);
        PlotCell(targetTexture, cellIndex, bitmap);
    }

    public static void PlotCell(RetroTextureVertical targetTexture, int cellIndex, Bitmap bitmap)
    {
        var targetHeight = targetTexture.Width;
        var targetStart = bitmap.Height * cellIndex;
        var index = 0;
        var targetPixels = new Color[targetTexture.Width * targetTexture.Height];

        targetTexture.GetData(targetPixels);

        for (var y = 0; y < targetHeight; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                if (x >= targetStart && x < targetStart + bitmap.Width)
                    if (bitmap.GetPixel(x - targetStart, y).A > 0)
                        targetPixels[index] = bitmap.GetPixel(x - targetStart, y);

                index++;
            }
        }

        targetTexture.SetData(targetPixels);
    }

    public void SetData(Texture2D source)
    {
        var data = new Color[source.Width * source.Height];
        source.GetData(data);
        SetData(data);
    }

    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y) =>
        spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(0, cellIndex * CellHeight, CellWidth, CellHeight), Color.White);

    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, Color color) =>
        spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(0, cellIndex * CellHeight, CellWidth, CellHeight), color);

    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, ColorPalette color) =>
        spriteBatch.Draw(
            this,
            new Vector2(x, y),
            new Rectangle(0, cellIndex * CellHeight, CellWidth, CellHeight),
            ColorPaletteHelper.GetColor(color)
        );
}