using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
    public class CollisionTexture : RetroTexture
    {
        public List<Rectangle> CollisionZones { get; }
        
        public CollisionTexture(GraphicsDevice graphicsDevice, Point cellSize, int cellCount) : base(graphicsDevice, cellSize, cellCount)
        {
            CollisionZones = new List<Rectangle>();
        }

        public CollisionTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth, cellHeight, cellCount)
        {
            CollisionZones = new List<Rectangle>();
        }

        public bool Intersects(Rectangle rectangle) =>
            CollisionZones.Any(collisionZone => collisionZone.Intersects(rectangle));

        public bool Intersects(IEnumerable<Rectangle> rectangles) =>
            (
                from collisionZone in CollisionZones
                from rectangle in rectangles
                where collisionZone.Intersects(rectangle)
                select collisionZone
            ).Any();
    }
}