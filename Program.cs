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

                gameOverScreen.draw(score);

                // * update part
                if (gameState == GameState.Playing)
                {
                    curSnake.update();
                    checkEggEaten();

                    if (!curSnake.isAlive) changeGameState(GameState.GameOver);
                }
                else if (gameState == GameState.GameOver)
                {
                    curSnake.updateDead();

                    if (!curSnake.hasHead && !gameOverScreen.isVisible)
                    {
                        gameOverScreen.turnOn();
                    }

                    gameOverScreen.update();

                    if (gameOverScreen.s_requestStartNewGame)
                    {
                        changeGameState(GameState.Playing);
                        randomizeEggPos();
                        curSnake = new Snake();
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F)) changeGameState(GameState.GameOver);

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
        private static void changeGameState(GameState newState)
        {
            if (gameState == GameState.GameOver)
                gameOverScreen.turnOff();

            gameState = newState;

            if (newState == GameState.GameOver)
            {
                curSnake.destroyBody();
                // gameOverScreen.turnOn();
            }
        }
    }
}
