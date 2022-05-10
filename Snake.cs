using MathStuff;
using Raylib_cs;
using Canvas;

public class Snake : GameObject
{
    private System.Collections.Generic.List<Vec2f> body = new System.Collections.Generic.List<Vec2f>();
    public Vec2f head { get { return body[0]; } }
    public bool hasHead { get { return body.Count > 0; } }
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

        for (int i = 0; i < body.Count - 1; i++)
        {
            bool isNextBodyPartNear = false;

            // todo: fix this later, it's probably too slow
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    if (body[i + 1].x + x == body[i].x && body[i + 1].y + y == body[i].y)
                        isNextBodyPartNear = true;
                }
            }

            Vec2f lineStart = new Vec2f((int)body[i].x * Globals.cellWidth + Globals.cellWidth / 2, (int)body[i].y * Globals.cellHeight + Globals.cellHeight / 2);
            Vec2f lineEnd = new Vec2f((int)body[i + 1].x * Globals.cellWidth + Globals.cellWidth / 2, (int)body[i + 1].y * Globals.cellHeight + Globals.cellHeight / 2);
            if (isNextBodyPartNear)
                Pen.DrawLine(lineStart, lineEnd, Color.BLUE);
            else
            {
                Vec2f invDir = body[i] - body[i + 1];
                Vec2f offset = invDir * Globals.cellWidth;
                Pen.DrawLine(lineStart, lineStart + offset, Color.BLUE);
                Pen.DrawLine(lineEnd, lineEnd + offset.Inverse(), Color.BLUE);
            
                invDir = invDir.Inverse();

            }
        }
    }
    public override void update()
    {
        base.update();

        if (!isAlive || body.Count == 0) return;

        double curMs = Raylib.GetTime();

        if (curMs - lastMsWalked > 0.200f)
        {
            lastMsWalked = curMs;
            lastDir = curDir;

            step();
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
    private void step()
    {
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
    public bool isPosInBody(Vec2f pos)
    {
        foreach (Vec2f bodyPart in body)
            if (bodyPart == pos)
                return true;
        return false;
    }
    public void destroyBody()
    {
        body.RemoveAt(body.Count - 1);

        if (body.Count != 0)
            CreateTimer(0.1, destroyBody);
    }
}