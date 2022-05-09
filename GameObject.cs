using System;
using System.Collections.Generic;

using Raylib_cs;

public class GameObject
{
    private List<Timer> timers = new List<Timer>();
    public virtual void update()
    {
        // foreach (Timer timer in timers)
        for (int i = 0; i < timers.Count; i++)
            if (Raylib.GetTime() - timers[i].startTime > timers[i].duration)
                timers[i].action();
        
        timers.RemoveAll(t => Raylib.GetTime() - t.startTime > t.duration);
    }
    public void CreateTimer(double duration, Action action)
    {
        timers.Add(new Timer() { startTime = Raylib.GetTime(), duration = duration, action = action });
    }
    private struct Timer
    {
        public double startTime;
        public double duration;
        public Action action;
    }
}