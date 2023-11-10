namespace Engine.Graphics.Primitives;
using Engine.Data;
using Raylib_cs;
using System.Numerics;
public class Cube
{
    public Cube(Vector3 size, Color color, string texture)
    {
        mesh = Raylib.GenMeshCube(size.X, size.Y, size.Z);
        Size = size;
        Color = color;
        Texture = Textures.GetTexture(texture);
        //UV = size;
        model = Raylib.LoadModelFromMesh(mesh);
    }
    public Mesh mesh;
    public Model model;
    public Vector3 Position;
    public Vector3 Size;
    public Color Color;
    public Vector2 UV;
    public Texture2D Texture;
}
