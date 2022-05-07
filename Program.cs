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
            curEgg.position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
            while (curSnake.isPosInBody(curEgg.position))
            {
                curEgg.position = new Vec2f(Raylib.GetRandomValue(0, Globals.cellAmount - 1), Raylib.GetRandomValue(0, Globals.cellAmount - 1));
                Console.WriteLine("Tried to spawn inside snake body.");
            }
        }
    }
}
