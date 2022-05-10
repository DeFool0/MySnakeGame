using Raylib_cs;
using MathStuff;

namespace Canvas
{
    public class Pen
    {
        public static void DrawLine(Vec2f start, Vec2f end, Color color)
        {
            Raylib.DrawLine((int)start.x, (int)start.y, (int)end.x, (int)end.y, color);
        }
    }
}