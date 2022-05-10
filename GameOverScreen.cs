using System;

using Raylib_cs;

public class GameOverScreen
{
    // * signals
    public bool s_requestStartNewGame = false;

    // * variables
    public double initMs = 0;
    private enum States { On, Off }
    private States state = States.Off;
    public bool isVisible { get { return state == States.On; } }
    public void draw(Int64 score, Int64 highscore)
    {
        double elapsedMs = 0;
        if (state == States.On)
        {
            elapsedMs = Raylib.GetTime() - initMs;
            elapsedMs = Math.Min(elapsedMs, 1);
        }
        else if (state == States.Off)
        {
            elapsedMs = Raylib.GetTime() - initMs;
            elapsedMs = 1 - elapsedMs;
            elapsedMs = Math.Max(elapsedMs, 0);
        }

        int middleW = Globals.screenWidth / 2;
        int middleH = Globals.screenHeight / 2;

        Color fontColor = new Color(255, 255, 255, (int)(255 * elapsedMs));

        Raylib.DrawRectangle(0, 0, Globals.screenWidth, Globals.screenHeight, new Color(0, 0, 0, (int)(150 * elapsedMs)));
        Raylib.DrawText("GAME OVER", middleW - Raylib.MeasureText("GAME OVER", 50) / 2, middleH - middleH / 2, 50, fontColor);
        Raylib.DrawText("Score: " + score, middleW - Raylib.MeasureText("Score: " + score, 40) / 2, middleH, 40, fontColor);
        Raylib.DrawText("Highscore: " + highscore, middleW - Raylib.MeasureText("Highscore: " + highscore, 40) / 2, middleH + middleH / 2 / 2, 30, fontColor);
        Raylib.DrawText("Press ENTER to restart", middleW - Raylib.MeasureText("Press ENTER to restart", 40) / 2, middleH + middleH / 2, 40, fontColor);
    }
    public void update()
    {
        s_requestStartNewGame = Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER);
    }
    public void turnOff()
    {
        initMs = Raylib.GetTime();
        state = States.Off;
    }
    public void turnOn()
    {
        initMs = Raylib.GetTime();
        state = States.On;
    }
}