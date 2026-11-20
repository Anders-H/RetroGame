using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.RetroTextures;

public class CollisionTexture : RetroTexture
{
    public List<Rectangle>[] CollisionZones { get; }
        
    public CollisionTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCountX, int cellCountY) : this(graphicsDevice, cellSize.X, cellSize.Y, cellCountX, cellCountY)
    {
    }

    public CollisionTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCountX, int cellCountY) : base(graphicsDevice, cellWidth, cellHeight, cellCountX, cellCountY)
    {
        CollisionZones = new List<Rectangle>[CellCount];
            
        for(var i = 0; i < CellCount; i++)
            CollisionZones[i] = [];
    }

    public static CollisionTexture ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCountX, int cellCountY, Color color)
    {
        var size = (cellWidth*cellCountX)*(cellHeight*cellCountY);
        var pixels = new Color[size];

        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        var texture = new CollisionTexture(graphicsDevice, cellWidth, cellHeight, cellCountX, cellCountY);
        texture.SetData(pixels);
        return texture;
    }

    public void AddCollision(int cellIndex, Rectangle zone) =>
        CollisionZones[cellIndex].Add(zone);

    public void AddCollision(int cellIndex, int x, int y, int width, int height) =>
        CollisionZones[cellIndex].Add(new Rectangle(x, y, width, height));

    public bool Intersects(int cellIndex, Point myLocation, Rectangle rectangle, Point rectangleLocation)
    {
        rectangle.Offset(rectangleLocation);

        foreach (var zone in CollisionZones[cellIndex])
        {
            var translated = zone;
            translated.Offset(myLocation);

            if (translated.Intersects(rectangle))
                return true;
        }
        return false;
    }

    public bool Intersects(int cellIndex, Point myLocation, IEnumerable<Rectangle> rectangles, Point rectangleLocation)
    {
        foreach (var rectangle in rectangles)
            if (Intersects(cellIndex, myLocation, rectangle, rectangleLocation))
                return true;

        return false;
    }
}
