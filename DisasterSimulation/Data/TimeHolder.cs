using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

class TimeHolder
{
    DateTime StartTime, CurrentTime;

    public ulong frame = 0;
    public float runTime;

    public float deltaTime;

    public void Initialize()
    {
        StartTime = DateTime.Now;
    }

    public void Update()
    {
        frame++;
        deltaTime = (float)(DateTime.Now - CurrentTime).TotalSeconds;
        CurrentTime = DateTime.Now;
        runTime = (float)(CurrentTime - StartTime).TotalSeconds;
    }
}

