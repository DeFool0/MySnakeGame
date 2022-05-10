using System;

using Raylib_cs;
using Canvas;
using MathStuff;

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

        Pen.DrawRectangle(Vec2f.ZERO, Globals.screenSize, new Color(0, 0, 0, (int)(150 * elapsedMs)));
        Pen.DrawText("GAME OVER", new Vec2f(middleW - Raylib.MeasureText("GAME OVER", 50) / 2, middleH  / 2), 50, fontColor);
        Pen.DrawText("Score: " + score, new Vec2f(middleW - Raylib.MeasureText("Score: " + score, 40) / 2, middleH), 40, fontColor);
        Pen.DrawText("Highscore: " + highscore, new Vec2f(middleW - Raylib.MeasureText("Highscore: " + highscore, 40) / 2, middleH * 1.25f), 30, fontColor);
        Pen.DrawText("Press ENTER to restart", new Vec2f(middleW - Raylib.MeasureText("Press ENTER to restart", 40) / 2, middleH * 1.5f), 40, fontColor);
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