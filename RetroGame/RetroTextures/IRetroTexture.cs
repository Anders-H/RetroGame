using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.RetroTextures;

public interface IRetroTexture
{
    int Width { get; }
    int Height { get; }
    int CellWidth { get; }
    int CellHeight { get; }
    int CellCount { get; }
    void SetData(Texture2D source);
    void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y);
    void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, Color color);
    void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, ColorPalette color);
}