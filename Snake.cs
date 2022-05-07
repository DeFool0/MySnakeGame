using MathStuff;
using Raylib_cs;

public class Snake
{
    private System.Collections.Generic.List<Vec2f> body = new System.Collections.Generic.List<Vec2f>();
    public Vec2f head { get { return body[0]; } }
    private Vec2f curDir = new Vec2f(-1, 0);
    private Vec2f lastDir;
    private double lastMsWalked = 0;
    public bool isAlive { get; private set; } = true;
    public Snake()
    {
        body.Add(new Vec2f(15, 15));
    }
    public void draw()
    {
        foreach (Vec2f bodyPart in body)
            Raylib.DrawRectangle((int)bodyPart.x * Globals.cellWidth, (int)bodyPart.y * Globals.cellHeight, Globals.cellWidth, Globals.cellHeight, Color.GREEN);
    }
    public void update()
    {
        double curMs = Raylib.GetTime();

        if (curMs - lastMsWalked > 0.200f)
        {
            lastMsWalked = curMs;
            lastDir = curDir;

            for (int i = body.Count - 1; i > 0; i--)
                body[i] = body[i - 1];

            body[0] += curDir;

            for (int i = 1; i < body.Count; i++)
                if (body[0] == body[i])
                    isAlive = false;

            if (body[0].x < 0)
                body[0] = new Vec2f(Globals.cellAmount - 1, body[0].y);
            else if (body[0].x > Globals.cellAmount - 1)
                body[0] = new Vec2f(0, body[0].y);

            if (body[0].y < 0)
                body[0] = new Vec2f(body[0].x, Globals.cellAmount - 1);
            else if (body[0].y > Globals.cellAmount - 1)
                body[0] = new Vec2f(body[0].x, 0);
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            if (lastDir.y != 1)
                curDir = new Vec2f(0, -1);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            if (lastDir.y != -1)
                curDir = new Vec2f(0, 1);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            if (lastDir.x != 1)
                curDir = new Vec2f(-1, 0);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            if (lastDir.x != -1)
                curDir = new Vec2f(1, 0);
    }
    public void grow()
    {
        body.Add(body[body.Count - 1].copy());
    }
    public bool isPosInBody(Vec2f pos)
    {
        foreach (Vec2f bodyPart in body)
            if (bodyPart == pos)
                return true;
        return false;
    }
}