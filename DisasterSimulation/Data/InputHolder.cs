using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// A base class for monitoring keys.
/// </summary>
class MonitorKey
{
    public bool Pressed = false;
    public bool Held = false;
    public bool Delay = false;
    public float Read = -1;
}

/// <summary>
/// A class to monitor the input of the user.
/// </summary>
class InputHolder
{
    Data data;

    public bool EscapeKeyPressed = false;

    private Dictionary<Keyboard.Key, MonitorKey> RegisteredKeys;

    /// <summary>
    /// Prepares the data structures.
    /// </summary>
    /// <param name="d">A reference to the primary data structure for the application.</param>
    public void Initialize(Data d)
    {
        data = d;
        RegisteredKeys = new Dictionary<Keyboard.Key, MonitorKey>();
    }

    /// <summary>
    /// Tells the input class to begin tracking a key.
    /// </summary>
    /// <param name="k">The key to track.</param>
    public void TrackKey(Keyboard.Key k)
    {
        if(!RegisteredKeys.ContainsKey(k))
        {
            RegisteredKeys.Add(k, new MonitorKey());
        }
        //RegisteredKeys.Add(k, new MonitorKey());
    }

    /// <summary>
    /// Tells the input class to stop tracking a key.
    /// </summary>
    /// <param name="k">The key to stop tracking.</param>
    public void UntrackKey(Keyboard.Key k)
    {
        RegisteredKeys.Remove(k);
    }

    /// <summary>
    /// Tells the input class to stop tracking all keys.
    /// </summary>
    public void ClearTrackedKeys()
    {
        RegisteredKeys.Clear();
    }

    /// <summary>
    /// Checks if a key is currently being held down.
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>True if the key is being held down.</returns>
    public bool CheckKeyHeld(Keyboard.Key k)
    {
        if (!RegisteredKeys.ContainsKey(k))
            return false;

        return RegisteredKeys[k].Held;
    }

    /// <summary>
    /// Checks if the key has been pressed and released.
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>Returns true if the key has been pressed.</returns>
    public bool CheckKeyPressed(Keyboard.Key k)
    {
        if (!RegisteredKeys.ContainsKey(k))
            return false;

        if(RegisteredKeys[k].Pressed)
        {
            RegisteredKeys[k].Pressed = false;
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks all tracked keys for updates.
    /// </summary>
    public void Update()
    {
        if(!data.Graphics.ProgramWindow.HasFocus())
        {
            return;
        }

        if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
        {
            EscapeKeyPressed = true;
            return;
        }

        

        List<Keyboard.Key> KeyList = new List<Keyboard.Key>(RegisteredKeys.Keys);
        foreach (Keyboard.Key key in KeyList)
        {
            if(Keyboard.IsKeyPressed(key))
            {
                RegisteredKeys[key].Held = true;
                if (!RegisteredKeys[key].Delay && (data.Time.runTime - RegisteredKeys[key].Read) > data.Settings.KeyPressDelay)
                {
                    RegisteredKeys[key].Pressed = true;
                    RegisteredKeys[key].Delay = true;
                    RegisteredKeys[key].Read = data.Time.runTime;
                }

            } else
            {
                RegisteredKeys[key].Held = false;
                RegisteredKeys[key].Delay = false;
            }
        }
    }
}
