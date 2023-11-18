namespace Engine.Systems.LDtk;

using System;
using System.Numerics;
using Engine.Utils;

/// <summary> LDtk IntGrid </summary>
public class LDtkIntGrid
{
    /// <summary> Size of a tile in pixels </summary>
    public int TileSize { get; set; }

    /// <summary> The underlying values of the int grid </summary>
    public int[] Values { get; set; }

    /// <summary> Worldspace start Position of the intgrid </summary>
    public Point WorldPosition { get; set; }

    /// <summary> Worldspace start Position of the intgrid </summary>
    public Point GridSize { get; set; }

    /// <summary> Used by json deserializer not for use by user! </summary>
#pragma warning disable CS8618
    public LDtkIntGrid() { }
#pragma warning restore

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(int x, int y)
    {
        if (Values.Length == 0)
        {
            return 0;
        }

        if (Contains(new Point(x, y)))
        {
            return Values[(y * GridSize.x) + x];
        }
        else
        {
            return 0;
        }
    }

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(Point position)
    {
        return GetValueAt(position.x, position.y);
    }

    /// <summary> Gets the int value at location and return 0 if out of bounds </summary>
    public int GetValueAt(Vector2 position)
    {
        return GetValueAt((int)position.X, (int)position.Y);
    }

    /// <summary> Check if point is inside of a grid </summary>
    public bool Contains(Point point)
    {
        return point.x >= 0 && point.y >= 0 && point.x < GridSize.x && point.y < GridSize.y;
    }

    /// <summary> Check if point is inside of a grid </summary>
    public bool Contains(Vector2 point)
    {
        return point.X >= 0 && point.Y >= 0 && point.X < GridSize.x && point.Y < GridSize.y;
    }

    /// <summary> Convert from world pixel space to int grid space Floors the value based on <see cref="TileSize"/> to an Integer </summary>
    public Point FromWorldToGridSpace(Vector2 position)
    {
        int x = (int)Math.Floor(position.X / TileSize);
        int y = (int)Math.Floor(position.Y / TileSize);
        return new Point(x, y);
    }
}
