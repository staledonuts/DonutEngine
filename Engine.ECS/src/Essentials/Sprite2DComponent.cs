using System.Numerics;
using Engine.Data;
using Raylib_cs;

namespace Engine.Systems.ECS;

public class Sprite2DComponent : Component
{
    public Sprite2DComponent(string textureName)
    {
        if(Textures.TryGetTexture(textureName, out Texture))
        {
            ImageRect = new(0,0, Texture.Width, Texture.Height);
        }
        else
        {
            ImageRect = new(0,0,1,1);
        }
        color = Color.RAYWHITE;
    }

    public Texture2D Texture;
    public Color color;
    public Rectangle ImageRect;

    public void DrawSprite()
    {
        Raylib.DrawTexturePro(Texture, ImageRect, ImageRect, Entity.transform.Position, Entity.transform.Rotation, color);
    }
    public override void OnComponentAdded()
    {
        
    }
    public override void OnComponentRemoved()
    {
        
    }
}