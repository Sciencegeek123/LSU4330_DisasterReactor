using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class MonitorKey
{
    public bool Pressed = false;
    public bool Held = false;
    public bool Delay = false;
    public float Read = -1;
}

class InputHolder
{
    Data data;

    public bool EscapeKeyPressed = false;

    private Dictionary<Keyboard.Key, MonitorKey> RegisteredKeys;

    public void Initialize(Data d)
    {
        data = d;
        RegisteredKeys = new Dictionary<Keyboard.Key, MonitorKey>();
    }

    public void TrackKey(Keyboard.Key k)
    {
        if(!RegisteredKeys.ContainsKey(k))
        {
            RegisteredKeys.Add(k, new MonitorKey());
        }
        //RegisteredKeys.Add(k, new MonitorKey());
    }

    public void UntrackKey(Keyboard.Key k)
    {
        RegisteredKeys.Remove(k);
    }

    public void ClearTrackedKeys()
    {
        RegisteredKeys.Clear();
    }

    public bool CheckKeyHeld(Keyboard.Key k)
    {
        if (!RegisteredKeys.ContainsKey(k))
            return false;

        return RegisteredKeys[k].Held;
    }

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
