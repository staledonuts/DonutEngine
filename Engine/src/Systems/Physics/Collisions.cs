using System;
using System.Numerics;

namespace Engine.FlatPhysics;

public static class Collisions
{
    public static bool IntersectCirclePolygon(Vector2 circleCenter, float circleRadius, Vector2 polygonCenter, Vector2[] vertices, out Vector2 normal, out float depth)
    {
        normal = Vector2.Zero;
        depth = float.MaxValue;

        Vector2 axis = Vector2.Zero;
        float axisDepth = 0f;
        float minA, maxA, minB, maxB;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 va = vertices[i];
            Vector2 vb = vertices[(i + 1) % vertices.Length];

            Vector2 edge = vb - va;
            axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(vertices, axis, out minA, out maxA);
            Collisions.ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        int cpIndex = Collisions.FindClosestPointOnPolygon(circleCenter, vertices);
        Vector2 cp = vertices[cpIndex];

        axis = cp - circleCenter;
        axis = FlatMath.Normalize(axis);

        Collisions.ProjectVertices(vertices, axis, out minA, out maxA);
        Collisions.ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

        if (minA >= maxB || minB >= maxA)
        {
            return false;
        }

        axisDepth = MathF.Min(maxB - minA, maxA - minB);

        if (axisDepth < depth)
        {
            depth = axisDepth;
            normal = axis;
        }

        Vector2 direction = polygonCenter - circleCenter;

        if (FlatMath.Dot(direction, normal) < 0f)
        {
            normal = -normal;
        }

        return true;
    }


    public static bool IntersectCirclePolygon(Vector2 circleCenter, float circleRadius, 
        Vector2[] vertices, 
        out Vector2 normal, out float depth)
    {
        normal = Vector2.Zero;
        depth = float.MaxValue;

        Vector2 axis = Vector2.Zero;
        float axisDepth = 0f;
        float minA, maxA, minB, maxB;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 va = vertices[i];
            Vector2 vb = vertices[(i + 1) % vertices.Length];

            Vector2 edge = vb - va;
            axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(vertices, axis, out minA, out maxA);
            Collisions.ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        int cpIndex = Collisions.FindClosestPointOnPolygon(circleCenter, vertices);
        Vector2 cp = vertices[cpIndex];

        axis = cp - circleCenter;
        axis = FlatMath.Normalize(axis);

        Collisions.ProjectVertices(vertices, axis, out minA, out maxA);
        Collisions.ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

        if (minA >= maxB || minB >= maxA)
        {
            return false;
        }

        axisDepth = MathF.Min(maxB - minA, maxA - minB);

        if (axisDepth < depth)
        {
            depth = axisDepth;
            normal = axis;
        }

        Vector2 polygonCenter = Collisions.FindArithmeticMean(vertices);

        Vector2 direction = polygonCenter - circleCenter;

        if (FlatMath.Dot(direction, normal) < 0f)
        {
            normal = -normal;
        }

        return true;
    }

    private static int FindClosestPointOnPolygon(Vector2 circleCenter, Vector2[] vertices)
    {
        int result = -1;
        float minDistance = float.MaxValue;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector2 v = vertices[i];
            float distance = FlatMath.Distance(v, circleCenter);

            if(distance < minDistance)
            {
                minDistance = distance;
                result = i;
            }
        }

        return result;
    }

    private static void ProjectCircle(Vector2 center, float radius, Vector2 axis, out float min, out float max)
    {
        Vector2 direction = FlatMath.Normalize(axis);
        Vector2 directionAndRadius = direction * radius;

        Vector2 p1 = center + directionAndRadius;
        Vector2 p2 = center - directionAndRadius;

        min = FlatMath.Dot(p1, axis);
        max = FlatMath.Dot(p2, axis);

        if(min > max)
        {
            // swap the min and max values.
            float t = min;
            min = max;
            max = t;
        }
    }

    public static bool IntersectPolygons(Vector2 centerA, Vector2[] verticesA, Vector2 centerB, Vector2[] verticesB, out Vector2 normal, out float depth)
    {
        normal = Vector2.Zero;
        depth = float.MaxValue;

        for (int i = 0; i < verticesA.Length; i++)
        {
            Vector2 va = verticesA[i];
            Vector2 vb = verticesA[(i + 1) % verticesA.Length];

            Vector2 edge = vb - va;
            Vector2 axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(verticesA, axis, out float minA, out float maxA);
            Collisions.ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        for (int i = 0; i < verticesB.Length; i++)
        {
            Vector2 va = verticesB[i];
            Vector2 vb = verticesB[(i + 1) % verticesB.Length];

            Vector2 edge = vb - va;
            Vector2 axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(verticesA, axis, out float minA, out float maxA);
            Collisions.ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        Vector2 direction = centerB - centerA;

        if (FlatMath.Dot(direction, normal) < 0f)
        {
            normal = -normal;
        }

        return true;
    }

    public static bool IntersectPolygons(Vector2[] verticesA, Vector2[] verticesB, out Vector2 normal, out float depth)
    {
        normal = Vector2.Zero;
        depth = float.MaxValue;

        for(int i = 0; i < verticesA.Length; i++)
        {
            Vector2 va = verticesA[i];
            Vector2 vb = verticesA[(i + 1) % verticesA.Length];

            Vector2 edge = vb - va;
            Vector2 axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(verticesA, axis, out float minA, out float maxA);
            Collisions.ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if(minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if(axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        for (int i = 0; i < verticesB.Length; i++)
        {
            Vector2 va = verticesB[i];
            Vector2 vb = verticesB[(i + 1) % verticesB.Length];

            Vector2 edge = vb - va;
            Vector2 axis = new Vector2(-edge.Y, edge.X);
            axis = FlatMath.Normalize(axis);

            Collisions.ProjectVertices(verticesA, axis, out float minA, out float maxA);
            Collisions.ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        Vector2 centerA = Collisions.FindArithmeticMean(verticesA);
        Vector2 centerB = Collisions.FindArithmeticMean(verticesB);

        Vector2 direction = centerB - centerA;

        if(FlatMath.Dot(direction, normal) < 0f)
        {
            normal = -normal;
        }

        return true;
    }

    private static Vector2 FindArithmeticMean(Vector2[] vertices)
    {
        float sumX = 0f;
        float sumY = 0f;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector2 v = vertices[i];
            sumX += v.X;
            sumY += v.Y;
        }

        return new Vector2(sumX / (float)vertices.Length, sumY / (float)vertices.Length);
    }

    private static void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector2 v = vertices[i];
            float proj = FlatMath.Dot(v, axis);

            if(proj < min) { min = proj; }
            if(proj > max) { max = proj; }
        }
    }

    public static bool IntersectCircles(
        Vector2 centerA, float radiusA, 
        Vector2 centerB, float radiusB, 
        out Vector2 normal, out float depth)
    {
        normal = Vector2.Zero;
        depth = 0f;

        float distance = FlatMath.Distance(centerA, centerB);
        float radii = radiusA + radiusB;

        if(distance >= radii)
        {
            return false;
        }

        normal = FlatMath.Normalize(centerB - centerA);
        depth = radii - distance;

        return true;
    }

}
