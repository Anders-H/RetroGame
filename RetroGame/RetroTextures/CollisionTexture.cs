using System.Collections.Generic;
using System.Linq;
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

    public new static CollisionTexture ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCountX, int cellCountY, Color color)
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
        var myRectangles = new Rectangle[CollisionZones[cellIndex].Count];

        for (var i = 0; i < CollisionZones[cellIndex].Count; i++)
        {
            var r = CollisionZones[cellIndex][i];

            myRectangles[i] = new Rectangle(
                r.X + myLocation.X,
                r.Y + myLocation.Y,
                r.Width,
                r.Height
            );
        }

        var otherRectangle = new Rectangle(
            rectangle.X + rectangleLocation.X,
            rectangle.Y + rectangleLocation.Y,
            rectangle.Width,
            rectangle.Height
        );
            
        return myRectangles.Any(z => z.Intersects(otherRectangle));
    }

    public bool Intersects(int cellIndex, Point myLocation, IEnumerable<Rectangle> rectangles, Point rectangleLocation) =>
        rectangles.Any(otherRectangle => Intersects(cellIndex, myLocation, otherRectangle, rectangleLocation));
}