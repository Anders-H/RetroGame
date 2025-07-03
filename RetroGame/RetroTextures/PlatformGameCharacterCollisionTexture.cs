using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.RetroTextures;

public class PlatformGameCharacterCollisionTexture : RetroTexture
{
        
    public Rectangle[] BodyZone { get; set; }
    public Rectangle[] FeetZone { get; set; }
        
    public PlatformGameCharacterCollisionTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : base(graphicsDevice, cellSize, cellCount, 1)
    {
        BodyZone = new Rectangle[cellCount];
        FeetZone = new Rectangle[cellCount];
    }

    public PlatformGameCharacterCollisionTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth, cellHeight, cellCount, 1)
    {
        BodyZone = new Rectangle[cellCount];
        FeetZone = new Rectangle[cellCount];
    }

    public bool Intersects(int cellIndex, Point myLocation, Rectangle rectangle, Point rectangleLocation) =>
        BodyIntersects(cellIndex, myLocation, rectangle, rectangleLocation)
        || FeetIntersects(cellIndex, myLocation, rectangle, rectangleLocation);

    public bool BodyIntersects(int cellIndex, Point myLocation, Rectangle rectangle, Point rectangleLocation)
    {
        var b = BodyZone[cellIndex];
            
        var body = new Rectangle(
            b.X + myLocation.X,
            b.Y + myLocation.Y,
            b.Width,
            b.Height
        );
            
        var otherRectangle = new Rectangle(
            rectangle.X + rectangleLocation.X,
            rectangle.Y + rectangleLocation.Y,
            rectangle.Width,
            rectangle.Height
        );

        return body.Intersects(otherRectangle);
    }
        
    public bool FeetIntersects(int cellIndex, Point myLocation, Rectangle rectangle, Point rectangleLocation)
    {
        var f = FeetZone[cellIndex];
            
        var feet = new Rectangle(
            f.X + myLocation.X,
            f.Y + myLocation.Y,
            f.Width,
            f.Height
        );
            
        var otherRectangle = new Rectangle(
            rectangle.X + rectangleLocation.X,
            rectangle.Y + rectangleLocation.Y,
            rectangle.Width,
            rectangle.Height
        );

        return feet.Intersects(otherRectangle);
    }
}