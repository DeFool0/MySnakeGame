using MathStuff;
using Raylib_cs;
using Canvas;

public class Egg
{
    public Vec2f position = new Vec2f();
    public void draw()
    {
        Pen.DrawRectangle(position * Globals.cellWidth, Globals.cellSize, Color.RED);
    }
    public void newRandomPos()
    {
        position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
    }
}