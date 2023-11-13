namespace Engine.Graphics.Primitives;
using Engine.Assets;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
public class Quad
{
    public Quad(Vector3 size, Color color, string texture)
    {
        mesh = Raylib.GenMeshPlane(size.X, size.Y, 1,1);
        Size = size;
        Color = color;
        Texture = Textures.GetTexture(texture);
        //UV = size;
        model = LoadModelFromMesh(mesh);
    }
    public Mesh mesh;
    public Model model;
    public Vector3 Position;
    public Vector3 Size;
    public Color Color;
    public Vector2 UV;
    public Texture2D Texture;
}
