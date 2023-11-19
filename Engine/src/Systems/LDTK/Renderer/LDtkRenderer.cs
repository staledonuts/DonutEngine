namespace Engine.Systems.LDtk.Renderer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Engine.Assets;
using Engine.Utils;
using Raylib_cs;


/// <summary>
/// Renderer for the ldtkWorld, ldtkLevel, intgrids and entities.
/// This can all be done in your own class if you want to reimplement it and customize it differently
/// this one is mostly here to get you up and running quickly.
/// </summary>
public class LDtkRenderer
{
    Dictionary<string, RenderedLevel> PrerenderedLevels { get; set; } = new();
    /// <summary> The levels identifier to layers Dictionary </summary>
    Dictionary<string, Texture2D> TilemapCache { get; set; } = new();

    Texture2D pixel;

    /// <summary> This is used to intizialize the renderer for use with direct file loading </summary>
    public LDtkRenderer()
    {
        pixel = Textures.GetTexture("empty");
    }

    /// <summary> Dispose </summary>
    ~LDtkRenderer()
    {
        Raylib.UnloadTexture(pixel);
    }
    
    /// <summary> Prerender out the level to textures to optimize the rendering process </summary>
    /// <param name="level">The level to prerender</param>
    /// <exception cref="Exception">The level already has been prerendered</exception>
    public void PrerenderLevel(LDtkLevel level)
    {
        if (PrerenderedLevels.ContainsKey(level.Identifier))
        {
            return;
        }

        RenderedLevel renderLevel = new();
        renderLevel.Layers = RenderLayers(level);
        PrerenderedLevels.Add(level.Identifier, renderLevel);
    }

    Texture2D[] RenderLayers(LDtkLevel level)
    {
        List<Texture2D> layers = new();

        if (level.BgRelPath != null)
        {
            layers.Add(RenderBackgroundToLayer(level));
        }

        // Render Tile, Auto and Int grid layers
        for (int i = level.LayerInstances.Length - 1; i >= 0; i--)
        {
            LayerInstance layer = level.LayerInstances[i];

            if (layer._TilesetRelPath == null)
            {
                continue;
            }

            if (layer._Type == LayerType.Entities)
            {
                continue;
            }

            Texture2D texture = GetTexture(level, layer._TilesetRelPath);

            int width = layer._CWid * layer._GridSize;
            int height = layer._CHei * layer._GridSize;

            switch (layer._Type)
            {
                case LayerType.Tiles:
                foreach (TileInstance tile in layer.GridTiles.Where(tile => layer._TilesetDefUid.HasValue))
                {
                    Vector2 position = new(tile.Px.x + layer._PxTotalOffsetX, tile.Px.y + layer._PxTotalOffsetY);
                    Rectangle rect = new(tile.Src.x, tile.Src.y, layer._GridSize, layer._GridSize);
                    //SpriteEffects mirror = (SpriteEffects)tile.F;
                    Raylib.DrawTexturePro(texture, rect, rect, position, 0f, Color.WHITE);
                }
                break;

                case LayerType.AutoLayer:
                case LayerType.IntGrid:
                if (layer.AutoLayerTiles.Length > 0)
                {
                    foreach (TileInstance tile in layer.AutoLayerTiles.Where(tile => layer._TilesetDefUid.HasValue))
                    {
                        Vector2 position = new(tile.Px.x + layer._PxTotalOffsetX, tile.Px.y + layer._PxTotalOffsetY);
                        Rectangle rect = new(tile.Src.x, tile.Src.y, layer._GridSize, layer._GridSize);
                        //SpriteEffects mirror = (SpriteEffects)tile.F;
                        Raylib.DrawTexturePro(texture, rect, rect, position, 0, Color.WHITE);
                    }
                }
                break;

                case LayerType.Entities:
                default:
                break;
            }
        }

        return layers.ToArray();
    }

    Texture2D RenderBackgroundToLayer(LDtkLevel level)
    {
        Texture2D texture = GetTexture(level, level.BgRelPath);
        LevelBackgroundPosition bg = level._BgPos;
        Vector2 pos = bg.TopLeftPx.ToVector2();
        Raylib.DrawTexturePro(texture, pos, new Rectangle((int)bg.CropRect[0], (int)bg.CropRect[1], (int)bg.CropRect[2], (int)bg.CropRect[3]), Color.WHITE, 0, Vector2.Zero, bg.Scale, SpriteEffects.None, 0);

        //return layer;
    }

    Texture2D GetTexture(LDtkLevel level, string path)
    {
        if (TilemapCache.TryGetValue(path, out Texture2D texture))
        {
            return texture;
        }

        Texture2D tilemap;
        string file = Path.ChangeExtension(path, null);
        string directory = Path.GetDirectoryName(level.WorldFilePath)!;
        string assetName = Path.Join(directory, file);
        tilemap = Textures.GetTexture(assetName);

        TilemapCache.Add(path, tilemap);

        return tilemap;
    }

    /// <summary> Render the prerendered level you created from PrerenderLevel() </summary>
    public void RenderPrerenderedLevel(LDtkLevel level)
    {
        if (PrerenderedLevels.TryGetValue(level.Identifier, out RenderedLevel prerenderedLevel))
        {
            for (int i = 0; i < prerenderedLevel.Layers.Length; i++)
            {
                Raylib.DrawTextureEx(prerenderedLevel.Layers[i], level.Position.ToVector2(), 0, 1, Color.WHITE);
            }
        }
        else
        {
            throw new LDtkException($"No prerendered level with Identifier {level.Identifier} found.");
        }
    }

    /// <summary> Render the level directly without prerendering the layers alot slower than prerendering </summary>
    public void RenderLevel(LDtkLevel level)
    {
        ArgumentNullException.ThrowIfNull(level);
        Texture2D[] layers = RenderLayers(level);

        for (int i = 0; i < layers.Length; i++)
        {
            Raylib.DrawTextureEx(layers[i], level.Position.ToVector2(), 0, 1, Color.WHITE);
        }
    }

    /// <summary> Render intgrids by displaying the intgrid as solidcolor squares </summary>
    public void RenderIntGrid(LDtkIntGrid intGrid)
    {
        for (int x = 0; x < intGrid.GridSize.x; x++)
        {
            for (int y = 0; y < intGrid.GridSize.y; y++)
            {
                int cellValue = intGrid.Values[(y * intGrid.GridSize.x) + x];

                if (cellValue != 0)
                {

                    int spriteX = intGrid.WorldPosition.x + (x * intGrid.TileSize);
                    int spriteY = intGrid.WorldPosition.y + (y * intGrid.TileSize);
                    Rectangle tileRect = new(0, 0, spriteX, spriteY);
                    
                    Raylib.DrawTexturePro(pixel, tileRect, tileRect, tileRect.GetCenter(), 0, Color.PINK);
                }
            }
        }
    }

    /// <summary> Renders the entity with the tile it includes </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    public void RenderEntity<T>(T entity, Texture2D texture) where T : ILDtkEntity
    {
        //Raylib.DrawTexturePro(texture, entity.Position, entity.Tile, Color.WHITE, 0, entity.Pivot * entity.Size, 1, 0);
    }

    /// <summary> Renders the entity with the tile it includes </summary>
    /// <param name="entity">The entity you want to render</param>
    /// <param name="texture">The spritesheet/texture for rendering the entity</param>
    /// <param name="animationFrame">The current frame of animation. Is a very basic entity animation frames must be to the right of them and be the same size</param>
    public void RenderEntity<T>(T entity, Texture2D texture, int animationFrame) where T : ILDtkEntity
    {
        Rectangle animatedTile = entity.Tile;
        animatedTile.Offset(animatedTile.Width * animationFrame, 0);
        Raylib.DrawTexturePro(texture, animatedTile, animatedTile, animatedTile.GetCenter(), entity.Pivot.Y, Color.WHITE);
    }

}
