using System;
using System.Numerics;

namespace Engine.FlatPhysics
{
    internal readonly struct FlatTransform : IEquatable<FlatTransform>
    {
        public readonly float PositionX;
        public readonly float PositionY;
        public readonly float Sin;
        public readonly float Cos;

        public readonly static FlatTransform Zero = new FlatTransform(0f, 0f, 0f);

        public FlatTransform(Vector2 position, float angle)
        {
            this.PositionX = position.X;
            this.PositionY = position.Y;
            this.Sin = MathF.Sin(angle);
            this.Cos = MathF.Cos(angle);
        }

        public FlatTransform(float x, float y, float angle)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.Sin = MathF.Sin(angle);
            this.Cos = MathF.Cos(angle);
        }

        internal static Vector2 Transform(Vector2 v, FlatTransform transform)
        {
            return new Vector2(
                transform.Cos * v.X - transform.Sin * v.Y + transform.PositionX, 
                transform.Sin * v.X + transform.Cos * v.Y + transform.PositionY);
        }

        public bool Equals(FlatTransform other)
        {
            throw new NotImplementedException();
        }
    }
}
