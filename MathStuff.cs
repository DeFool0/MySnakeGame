namespace MathStuff
{
    public struct Vec2f
    {
        public float x {set; get;}
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
    }
}
