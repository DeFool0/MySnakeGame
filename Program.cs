using System;

using Raylib_cs;
using MathStuff;

namespace mySnakeClone
{

    public class Program
    {
        private static Snake curSnake = new Snake();
        private static Egg curEgg = new Egg();

        public static void Main()
        {
            Raylib.InitWindow(Globals.screenWidth, Globals.screenHeight, "Minha cobrona");

            while (!Raylib.WindowShouldClose() && curSnake.isAlive)
            {
                Raylib.BeginDrawing();

                drawBackGround();

                curEgg.draw();
                curSnake.draw();

                // * update part
                curSnake.update();
                checkEggEaten(curEgg, curSnake);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
        private static void drawBackGround()
        {
            Raylib.ClearBackground(Color.WHITE);

            for (int x = 0; x < Globals.screenWidth; x += Globals.cellWidth)
                for (int y = 0; y < Globals.screenHeight; y += Globals.cellHeight)
                    if (((x / Globals.cellWidth) + (y / Globals.cellHeight)) % 2 == 0)
                        Raylib.DrawRectangle(x, y, Globals.cellWidth, Globals.cellHeight, new Color(239, 237, 189, 255));
        }
        private static void checkEggEaten(Egg egg, Snake snake)
        {
            if (snake.head == egg.position)
            {
                snake.grow();

                egg.position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
            }
        }
    }
    public class Snake
    {
        private System.Collections.Generic.List<Vec2f> body = new System.Collections.Generic.List<Vec2f>();
        public Vec2f head { get { return body[0]; } }
        private Vec2f curDir = new Vec2f(-1, 0);
        private double lastMsWalked = 0;
        public bool isAlive = true;
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
                if (curDir.y != 1)
                    curDir = new Vec2f(0, -1);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                if (curDir.y != -1)
                    curDir = new Vec2f(0, 1);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                if (curDir.x != 1)
                    curDir = new Vec2f(-1, 0);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                if (curDir.x != -1)
                    curDir = new Vec2f(1, 0);

        }
        public void grow()
        {
            body.Add(body[body.Count - 1].copy());
        }
    }
    public class Egg
    {
        public Vec2f position = new Vec2f();
        public void draw()
        {
            Raylib.DrawRectangle((int)position.x * Globals.cellWidth, (int)position.y * Globals.cellHeight, Globals.cellWidth, Globals.cellHeight, Color.RED);
        }
    }
}
