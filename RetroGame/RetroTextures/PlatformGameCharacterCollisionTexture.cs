using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
    public class PlatformGameCharacterCollisionTexture : RetroTexture
    {
        
        public Rectangle BodyZone { get; set; }
        public Rectangle FeetZone { get; set; }
        
        public PlatformGameCharacterCollisionTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : base(graphicsDevice, cellSize, cellCount)
        {
        }

        public PlatformGameCharacterCollisionTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth, cellHeight, cellCount)
        {
        }

        //TODO: Location!
        
        public bool Intersects(Rectangle rectangle) =>
            BodyZone.Intersects(rectangle)
            || FeetZone.Intersects(rectangle);

        public bool Intersects(IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(
                rectangle => BodyZone.Intersects(rectangle) || FeetZone.Intersects(rectangle)
            );
        
        public bool Intersects(PlatformGameCharacterCollisionTexture texture) =>
            Intersects(texture.BodyZone)
            || Intersects(texture.FeetZone);

        public bool BodyIntersects(Rectangle rectangle) =>
            BodyZone.Intersects(rectangle);

        public bool BodyIntersects(IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(
                rectangle => BodyZone.Intersects(rectangle)
            );
        
        public bool BodyIntersects(PlatformGameCharacterCollisionTexture texture) =>
            BodyZone.Intersects(texture.BodyZone)
            || BodyZone.Intersects(texture.FeetZone);
        
        public bool FeetIntersects(Rectangle rectangle) =>
            FeetZone.Intersects(rectangle);

        public bool FeetIntersects(IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(
                rectangle => FeetZone.Intersects(rectangle)
            );

        public bool FeetIntersects(PlatformGameCharacterCollisionTexture texture) =>
            FeetZone.Intersects(texture.BodyZone)
            || FeetZone.Intersects(texture.FeetZone);
    }
}