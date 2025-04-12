using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.RetroTextures;

public class RetroTexture : Texture2D, IRetroTexture
{
    public int CellWidth { get; }
    public int CellHeight => Height;
    public int CellCount { get; }

    public RetroTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : this(graphicsDevice, cellSize.X, cellSize.Y, cellCount)
    {
    }
        
    public RetroTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth*cellCount, cellHeight)
    {
        CellWidth = cellWidth;
        CellCount = cellCount;
    }
        
    public static RetroTexture ScaffoldSimpleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) =>
        ScaffoldTextureCells(graphicsDevice, width, height, 1, color);
        
    public static RetroTexture ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color)
    {
        var size = cellWidth*cellCount*cellHeight;
        var pixels = new Color[size];

        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        var texture = new RetroTexture(graphicsDevice, cellWidth, cellHeight, cellCount);
        texture.SetData(pixels);
        return texture;
    }

    public void PlotCell(RetroTexture targetTexture, int cellIndex, ColorPalette background, string pixelData)
    {
        var bitmap = new Bitmap(CellWidth, CellHeight, background);
        bitmap.SetPixels(pixelData);
        PlotCell(targetTexture, cellIndex, bitmap);
    }
        
    public static void PlotCell(RetroTexture targetTexture, int cellIndex, Bitmap bitmap)
    {
        var targetWidth = targetTexture.Width;
        var targetStart = bitmap.Width*cellIndex;
        var index = 0;
        var targetPixels = new Color[targetTexture.Width*targetTexture.Height];
        targetTexture.GetData(targetPixels);

        for (var y = 0; y < bitmap.Height; y++)
        {
            for (var x = 0; x < targetWidth; x++)
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

    public static RetroTexture LoadContent(GraphicsDevice graphicsDevice, ContentManager content, int cellWidth, int cellHeight, int cellCount, string resourceName)
    {
        var result = new RetroTexture(graphicsDevice, cellWidth, cellHeight, cellCount);
        result.SetData(content.Load<Texture2D>(resourceName));
        return result;
    }

    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y) =>
        spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(cellIndex*CellWidth, 0, CellWidth, CellHeight), Color.White);
        
    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, Color color) =>
        spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(cellIndex* CellWidth, 0, CellWidth, CellHeight), color);
            
    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, ColorPalette color) =>
        spriteBatch.Draw(
            this,
            new Vector2(x, y),
            new Rectangle(cellIndex * CellWidth, 0, CellWidth, CellHeight),
            ColorPaletteHelper.GetColor(color)
        );

    public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, Flip flip)
    {
        var pos = new Rectangle(x, y, CellWidth, CellHeight);
        var rect = new Rectangle(cellIndex * CellWidth, 0, CellWidth, CellHeight);
        SpriteEffects effect;

        switch (flip)
        {
            case Flip.DoNotFlip:
                effect = SpriteEffects.None;
                break;
            case Flip.FlipLeftRight:
                effect = SpriteEffects.FlipHorizontally;
                break;
            case Flip.FlipUpDown:
                effect = SpriteEffects.FlipVertically;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(flip), flip, null);
        }

        spriteBatch.Draw(this, pos, rect, Color.White, 0, new Vector2(0, 0), effect, 0f);
    }

    public void DrawPart(SpriteBatch spriteBatch, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int destinationX, int destinationY)
    {
        if (sourceWidth > 0 && sourceHeight > 0)
            spriteBatch.Draw(this, new Vector2(destinationX, destinationY), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), Color.White);
    }

    public void DrawPart(SpriteBatch spriteBatch, Rectangle source, Vector2 destination)
    {
        if (source.Width > 0 && source.Height > 0)
            spriteBatch.Draw(this, destination, source, Color.White);
    }
}