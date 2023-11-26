namespace Engine.Utils.Extensions;
using Raylib_cs;
using System.Numerics;

public static class Texture2DExt
{

    public static Vector2 GetCenter(this Texture2D tex)
    {
        return new(tex.Width/2, tex.Height/2);
    }
	public static Vector2 CenterOffset(this Texture2D texture, Vector2 Pos)
    {   
        return new(Pos.X + (texture.Width * 0.5f), Pos.Y + (texture.Height * 0.5f));
    }
}