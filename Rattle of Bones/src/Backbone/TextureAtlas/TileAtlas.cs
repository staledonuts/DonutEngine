namespace DonutEngine.Backbone.Atlas;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;
using Box2DX.Common;


public class TileAtlas
{
    private Texture2D _texture;
    private Dictionary<int, Rectangle> _tileRectangles;

    public TileAtlas(Texture2D texture, int tileWidth, int tileHeight)
    {
        _texture = texture;
        _tileRectangles = new Dictionary<int, Rectangle>();

        int columns = texture.width / tileWidth;
        int rows = texture.height / tileHeight;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                int tileIndex = y * columns + x;
                Rectangle tileRectangle = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                _tileRectangles.Add(tileIndex, tileRectangle);
            }
        }
    }

    public Rectangle GetTileRectangle(int tileIndex)
    {
        return _tileRectangles[tileIndex];
    }

    public void DrawTile(int tileIndex, Vec2 position, Color tint)
    {
        Rectangle sourceRectangle = GetTileRectangle(tileIndex);
        Raylib.DrawTextureRec(_texture, sourceRectangle, new(position.X, position.Y), tint);
    }
}
