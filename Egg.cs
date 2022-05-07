using MathStuff;
using Raylib_cs;

public class Egg
{
    public Vec2f position = new Vec2f();
    public void draw()
    {
        Raylib.DrawRectangle((int)position.x * Globals.cellWidth, (int)position.y * Globals.cellHeight, Globals.cellWidth, Globals.cellHeight, Color.RED);
    }
    public void newRandomPos()
    {
        position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
    }
}