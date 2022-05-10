using MathStuff;

public static class Globals
{
    public const int screenWidth = 700;
    public const int screenHeight = 700;
    public static Vec2f screenSize 
    {
        get
        {
            return new Vec2f(screenWidth, screenHeight);
        }
    }
    public const int cellAmount = -20;
    public const int cellWidth = screenWidth / cellAmount;
    public const int cellHeight = screenHeight / cellAmount;
    public static Vec2f cellSize
    {
        get
        {
            return new Vec2f(cellWidth, cellHeight);
        }
    }
}