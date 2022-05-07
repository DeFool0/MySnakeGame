using System;

using Raylib_cs;
using MathStuff;

namespace mySnakeClone
{

    public class Program
    {
        private static Snake curSnake = new Snake();
        private static Egg curEgg = new Egg();
        private static GameOverScreen gameOverScreen = new GameOverScreen();
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

                if (gameState == GameState.GameOver) gameOverScreen.draw(score);

                // * update part
                if (gameState == GameState.Playing)
                {
                    curSnake.update();
                    checkEggEaten();

                    if (!curSnake.isAlive) gameState = GameState.GameOver;
                }
                else if (gameState == GameState.GameOver)
                {
                    gameOverScreen.update();

                    if (gameOverScreen.s_requestStartNewGame)
                    {
                        gameState = GameState.Playing;
                        randomizeEggPos();
                        curSnake = new Snake();
                    }
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
        private static void checkEggEaten()
        {
            if (curSnake.head == curEgg.position)
            {
                curSnake.grow();

                randomizeEggPos();

                score++;
            }
        }
        private static void randomizeEggPos()
        {
            curEgg.newRandomPos();
            while (curSnake.isPosInBody(curEgg.position)) curEgg.newRandomPos();
        }
    }
}
