using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
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

        public bool Intersects(int cellIndex, Rectangle rectangle) =>
            CollisionZones[cellIndex].Any(collisionZone => collisionZone.Intersects(rectangle));

        public bool Intersects(int cellIndex, IEnumerable<Rectangle> rectangles) =>
            (
                from collisionZone in CollisionZones
                from rectangle in rectangles
                where collisionZone[cellIndex].Intersects(rectangle)
                select collisionZone
            ).Any();
    }
}