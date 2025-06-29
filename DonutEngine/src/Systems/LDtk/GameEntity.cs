using Raylib_cs;
using System.Numerics;

namespace DonutEngine;

public abstract class GameEntity
{
    public Vector2 Position;
    public Vector2 Size;
    public Color Tint = Color.White;

    public GameEntity(Vector2 position)
    {
        this.Position = position;
    }
    public virtual void Update() { }
    public virtual void Draw() { }

    public virtual void ApplyLdtkFields(LdtkData.FieldInstance[] fieldInstances)
    {
        // Example:
        // foreach(var field in fieldInstances)
        // {
        //     if(field.Identifier == "Health")
        //     {
        //         this.Health = (int)(long)field.Value;
        //     }
        // }
    }
}

public class Player : GameEntity
{
    public Player(Vector2 position) : base(position)
    {
        // Player-specific initialization
        this.Size = new Vector2(16, 16); // Default size, could be overridden by LDtk fields
        this.Tint = Color.Blue;
    }

    public override void Update()
    {
        // --- Simple Player Movement ---
        float speed = 200 * Raylib.GetFrameTime();
        if(Raylib.IsKeyDown(KeyboardKey.W)) Position.Y -= speed;
        if(Raylib.IsKeyDown(KeyboardKey.S)) Position.Y += speed;
        if(Raylib.IsKeyDown(KeyboardKey.A)) Position.X -= speed;
        if(Raylib.IsKeyDown(KeyboardKey.D)) Position.X += speed;
    }

    public override void Draw()
    {
        // Draw a simple rectangle for the player
        Raylib.DrawRectangleV(Position, Size, Tint);
    }
}
