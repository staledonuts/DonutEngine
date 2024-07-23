using System;
using System.Numerics;

namespace Engine.FlatPhysics;

public enum ShapeType
{
    Circle = 0, 
    Box = 1
}

public class FlatBody
{
    private Vector2 position;
    private Vector2 linearVelocity;
    private float rotation;
    private float rotationalVelocity;

    private Vector2 force;

    public readonly float Density;
    public readonly float Mass;
    public readonly float InvMass;
    public readonly float Restitution;
    public readonly float Area;

    public float SizeMultiplier = 1;

    public readonly bool IsStatic;

    public Vector2 Size
    {
        get
        {
            return new(Width, Height);
        }
    }

    public readonly float Radius;
    public readonly float Width;
    public readonly float Height;

    private readonly Vector2[] vertices;
    public readonly int[] Triangles;
    private Vector2[] transformedVertices;
    private FlatAABB aabb;

    private bool transformUpdateRequired;
    private bool aabbUpdateRequired;

    public readonly ShapeType ShapeType;

    public Vector2 Position
    {
        get { return this.position; }
    }

    public float Rotation
    {
        get { return rotation; }
    }

    public Vector2 LinearVelocity
    {
        get { return linearVelocity; }
        internal set { linearVelocity = value; }
    }

    public FlatBody(Vector2 position, float density, float mass, float restitution, float area, 
        bool isStatic, float radius, float width, float height, float sizeMultiplier, ShapeType shapeType)
    {
        this.position = position;
        this.linearVelocity = Vector2.Zero;
        this.rotation = 0f;
        this.rotationalVelocity = 0f;

        this.force = Vector2.Zero;

        this.Density = density;
        this.Mass = mass;
        this.Restitution = restitution;
        this.Area = area;
        this.SizeMultiplier = sizeMultiplier;
        this.IsStatic = isStatic;
        this.Radius = radius;
        this.Width = width * SizeMultiplier;
        this.Height = height * SizeMultiplier;
        this.ShapeType = shapeType;

        if(!this.IsStatic)
        {
            this.InvMass = 1f / this.Mass;
        }
        else
        {
            this.InvMass = 0f;
        }

        if(this.ShapeType is ShapeType.Box)
        {
            this.vertices = FlatBody.CreateBoxVertices(this.Width, this.Height);
            this.Triangles = FlatBody.CreateBoxTriangles();
            this.transformedVertices = new Vector2[this.vertices.Length];
        }
        else
        {
            this.vertices = null;
            Triangles = null;
            this.transformedVertices = null;
        }

        this.transformUpdateRequired = true;
        this.aabbUpdateRequired = true;
    }

    private static Vector2[] CreateBoxVertices(float width, float height)
    {
        float left = -width / 2f;
        float right = left + width;
        float bottom = -height / 2f;
        float top = bottom + height;

        Vector2[] vertices = new Vector2[4];
        vertices[0] = new Vector2(left, top);
        vertices[1] = new Vector2(right, top);
        vertices[2] = new Vector2(right, bottom);
        vertices[3] = new Vector2(left, bottom);

        return vertices;
    }

    private static int[] CreateBoxTriangles()
    {
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
        return triangles;
    }

    public Vector2[] GetTransformedVertices()
    {
        if(transformUpdateRequired)
        {
            FlatTransform transform = new FlatTransform(position, rotation);

            for(int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                transformedVertices[i] = FlatTransform.Transform(v, transform);
            }
        }

        transformUpdateRequired = false;
        return transformedVertices;
    }

    public FlatAABB GetAABB()
    {
        if (aabbUpdateRequired)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            if (ShapeType is ShapeType.Box)
            {
                Vector2[] vertices = GetTransformedVertices();

                for (int i = 0; i < vertices.Length; i++)
                {
                    Vector2 v = vertices[i];

                    if (v.X < minX) { minX = v.X; }
                    if (v.X > maxX) { maxX = v.X; }
                    if (v.Y < minY) { minY = v.Y; }
                    if (v.Y > maxY) { maxY = v.Y; }
                }
            }
            else if (ShapeType is ShapeType.Circle)
            {
                minX = position.X - Radius;
                minY = position.Y - Radius;
                maxX = position.X + Radius;
                maxY = position.Y + Radius;
            }
            else
            {
                throw new Exception("Unknown ShapeType.");
            }

            aabb = new FlatAABB(minX, minY, maxX, maxY);
        }

        aabbUpdateRequired = false;
        return aabb;
    }

    internal void Step(float time, Vector2 gravity, int iterations)
    {
        if(IsStatic)
        {
            return;
        }

        time /= iterations;

        //force = mass * acc
        //acc = force / mass;

        Vector2 acceleration = force / Mass;
        linearVelocity += acceleration * time;


        linearVelocity += gravity * (2 *time);
        position += linearVelocity * time;

        rotation += rotationalVelocity * time;

        force = Vector2.Zero;
        transformUpdateRequired = true;
        aabbUpdateRequired = true;
    }

    public void Move(Vector2 amount)
    {
        position += amount;
        transformUpdateRequired = true;
        aabbUpdateRequired = true;
    }

    public void MoveTo(Vector2 position)
    {
        this.position = position;
        transformUpdateRequired = true;
        aabbUpdateRequired = true;
    }

    public void Rotate(float amount)
    {
        rotation += amount;
        transformUpdateRequired = true;
        aabbUpdateRequired = true;
    }

    public void AddForce(Vector2 amount)
    {
        force += amount;
    }

    public static bool CreateCircleBody(float radius, float sizeMultiplier, Vector2 position, float density, bool isStatic, float restitution, out FlatBody body, out string errorMessage)
    {
        body = null;
        errorMessage = string.Empty;

        float area = (radius * radius * MathF.PI) * sizeMultiplier;

        if(area < FlatWorld.MinBodySize)
        {
            errorMessage = $"Circle radius is too small. Min circle area is {FlatWorld.MinBodySize}.";
            return false;
        }

        if(area > FlatWorld.MaxBodySize)
        {
            errorMessage = $"Circle radius is too large. Max circle area is {FlatWorld.MaxBodySize}.";
            return false;
        }

        if (density < FlatWorld.MinDensity)
        {
            errorMessage = $"Density is too small. Min density is {FlatWorld.MinDensity}";
            return false;
        }

        if (density > FlatWorld.MaxDensity)
        {
            errorMessage = $"Density is too large. Max density is {FlatWorld.MaxDensity}";
            return false;
        }

        restitution = FlatMath.Clamp(restitution, 0f, 1f);

        // mass = area * depth * density
        float mass = area * density;

        body = new FlatBody(position, density, mass, restitution, area, isStatic, radius, 0f, 0f, sizeMultiplier, ShapeType.Circle);
        return true;
    }

    public static bool CreateBoxBody(float width, float height, float sizeMultiplier, Vector2 position, float density, bool isStatic, float restitution, out FlatBody body, out string errorMessage)
    {
        body = null;
        errorMessage = string.Empty;

        float area = width * height * sizeMultiplier;

        if (area < FlatWorld.MinBodySize)
        {
            errorMessage = $"Area is too small. Min area is {FlatWorld.MinBodySize}.";
            return false;
        }

        if (area > FlatWorld.MaxBodySize)
        {
            errorMessage = $"Area is too large. Max area is {FlatWorld.MaxBodySize}.";
            return false;
        }

        if (density < FlatWorld.MinDensity)
        {
            errorMessage = $"Density is too small. Min density is {FlatWorld.MinDensity}";
            return false;
        }

        if (density > FlatWorld.MaxDensity)
        {
            errorMessage = $"Density is too large. Max density is {FlatWorld.MaxDensity}";
            return false;
        }

        restitution = FlatMath.Clamp(restitution, 0f, 1f);

        // mass = area * depth * density
        float mass = area * density;

        body = new FlatBody(position, density, mass, restitution, area, isStatic, 0f, width, height, sizeMultiplier, ShapeType.Box);
        return true;
    }
}
