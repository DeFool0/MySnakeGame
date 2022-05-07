using System;

using Raylib_cs;

public class GameOverScreen
{
    public bool s_requestStartNewGame = false;
    public void draw(Int64 score)
    {
        int screenMiddleW = Globals.screenWidth / 2;
        int screenMiddleH = Globals.screenHeight / 2;

        Raylib.DrawRectangle(0, 0, Globals.screenWidth, Globals.screenHeight, new Color(0, 0, 0, 150));
        Raylib.DrawText("GAME OVER", screenMiddleW - Raylib.MeasureText("GAME OVER", 50) / 2, screenMiddleH - screenMiddleH / 2, 50, Color.WHITE);
        Raylib.DrawText("Score: " + score, screenMiddleW - Raylib.MeasureText("Score: " + score, 40) / 2, screenMiddleH, 40, Color.WHITE);
        Raylib.DrawText("Press ENTER to restart", screenMiddleW - Raylib.MeasureText("Press ENTER to restart", 40) / 2, screenMiddleH + screenMiddleH / 2, 40, Color.WHITE);
    }
    public void update()
    {
        s_requestStartNewGame = Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER);
    }
}