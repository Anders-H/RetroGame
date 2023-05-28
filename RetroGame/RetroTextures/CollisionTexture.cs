using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.RetroTextures;

public class CollisionTexture : RetroTexture
{
    public List<Rectangle>[] CollisionZones { get; }
        
    public CollisionTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : this(graphicsDevice, cellSize.X, cellSize.Y, cellCount)
    {
    }

    public CollisionTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth, cellHeight, cellCount)
    {
        CollisionZones = new List<Rectangle>[cellCount];
            
        for(var i = 0; i < cellCount; i++)
            CollisionZones[i] = new List<Rectangle>();
    }

    public new static CollisionTexture ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color)
    {
        var size = cellWidth*cellCount*cellHeight;
        var pixels = new Color[size];

        for (var i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        var texture = new CollisionTexture(graphicsDevice, cellWidth, cellHeight, cellCount);
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