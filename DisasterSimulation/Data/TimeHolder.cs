using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

/// <summary>
/// Contains the code used to manage time within the application.
/// 
/// This class is mostly obsolete, as the application uses a fixed delta time for the simulation.
/// 
/// However, it exists for legacy compatibility reasons, and for GUI 2.0.
/// </summary>
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

