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
        public static void DrawRectangle(Vec2f position, Vec2f size, Color color)
        {
            Raylib.DrawRectangle((int)position.x, (int)position.y, (int)size.x, (int)size.y, color);
        }
        public static void DrawText(string text, Vec2f position, int fontSize, Color color)
        {
            Raylib.DrawText(text, (int)position.x, (int)position.y, fontSize, color);
        }
    }
}