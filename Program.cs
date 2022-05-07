using System;

using Raylib_cs;
using MathStuff;

namespace mySnakeClone
{

    public class Program
    {
        private static Snake curSnake = new Snake();
        private static Egg curEgg = new Egg();
        private static Int64 score = 0;
        private enum GameState { Playing, GameOver };
        private static GameState gameState = GameState.Playing;

        public static void Main()
        {
            Raylib.InitWindow(Globals.screenWidth, Globals.screenHeight, "Minha cobrona");

            randomizeEggPos();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                drawBackGround();

                curEgg.draw();
                curSnake.draw();

                if (gameState == GameState.GameOver) drawGameOverScreen();

                // * update part
                if (gameState == GameState.Playing)
                {
                    curSnake.update();
                    checkEggEaten();

                    if (!curSnake.isAlive) gameState = GameState.GameOver;
                }
                else
                {
                    updateGameOverScreen();
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F)) gameState = GameState.GameOver;

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
        private static void drawGameOverScreen()
        {
            int screenMiddleW = Globals.screenWidth / 2;
            int screenMiddleH = Globals.screenHeight / 2;

            Raylib.DrawRectangle(0, 0, Globals.screenWidth, Globals.screenHeight, new Color(0, 0, 0, 150));
            Raylib.DrawText("GAME OVER", screenMiddleW - Raylib.MeasureText("GAME OVER", 50) / 2, screenMiddleH - screenMiddleH / 2, 50, Color.WHITE);
            Raylib.DrawText("Score: " + score, screenMiddleW - Raylib.MeasureText("Score: " + score, 40) / 2, screenMiddleH, 40, Color.WHITE);
            Raylib.DrawText("Press ENTER to restart", screenMiddleW - Raylib.MeasureText("Press ENTER to restart", 40) / 2, screenMiddleH + screenMiddleH / 2, 40, Color.WHITE);
        }
        private static void checkEggEaten()
        {
            if (curSnake.head == curEgg.position)
            {
                curSnake.grow();

                randomizeEggPos();

                score++;
            }
        }
        private static void updateGameOverScreen()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                gameState = GameState.Playing;
                curSnake = new Snake();
                
                randomizeEggPos();
            }
        }
        private static void randomizeEggPos()
        {
            curEgg.position = curSnake.head;
            while (curSnake.isPosInBody(curEgg.position))
            curEgg.position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
        }
    }
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
    public class Egg
    {
        public Vec2f position = new Vec2f();
        public void draw()
        {
            Raylib.DrawRectangle((int)position.x * Globals.cellWidth, (int)position.y * Globals.cellHeight, Globals.cellWidth, Globals.cellHeight, Color.RED);
        }
    }
}
