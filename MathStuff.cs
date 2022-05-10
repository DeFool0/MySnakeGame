using System;
using System.Diagnostics.CodeAnalysis;

namespace MathStuff
{
    public struct Vec2f
    {
        public float x;
        public float y;

        public Vec2f()
        {
            this.x = 0;
            this.y = 0;
        }
        public Vec2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vec2f(int x, int y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public static Vec2f operator +(Vec2f a, Vec2f b)
        {
            return new Vec2f(a.x + b.x, a.y + b.y);
        }
        public static Vec2f operator -(Vec2f a, Vec2f b)
        {
            return new Vec2f(a.x - b.x, a.y - b.y);
        }
        public static Vec2f operator *(int a, Vec2f b)
        {
            return new Vec2f(a * b.x, a * b.y);
        }
        public static Vec2f operator *(Vec2f b, int a)
        {
            return new Vec2f(a * b.x, a * b.y);
        }
        public static bool operator ==(Vec2f a, Vec2f b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Vec2f a, Vec2f b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public string toString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Vec2f copy()
        {
            return new Vec2f(x, y);
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (!(obj is Vec2f)) return false;
            Vec2f vObj= (Vec2f)obj;
            return x == vObj.x && y == vObj.y;
        }

        public Vec2f Inverse()
        {
            return new Vec2f(-x, -y);
        }
    }
}
