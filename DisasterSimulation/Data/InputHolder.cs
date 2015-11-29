using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


class InputHolder
{
    Data data;

    private Dictionary<Keyboard.Key, Tuple<bool,float>> RegisteredKeys;

    public void Initialize(Data d)
    {
        data = d;
        RegisteredKeys = new Dictionary<Keyboard.Key, Tuple<bool, float>>();
    }

    public void TrackKey(Keyboard.Key k)
    {
        RegisteredKeys.Add(k, new Tuple<bool, float>(false, -1));
    }

    public void UntrackKey(Keyboard.Key k)
    {
        RegisteredKeys.Remove(k);
    }

    public void ClearTrackedKeys(Keyboard.Key k)
    {
        RegisteredKeys.Clear();
    }

    public bool CheckKey(Keyboard.Key k)
    {
        bool ReturnValue = RegisteredKeys[k].Item1;
        RegisteredKeys[k] = new Tuple<bool, float>(false, RegisteredKeys[k].Item2);
        return ReturnValue;
    }

    public void Update()
    {
        List<Keyboard.Key> KeyList = new List<Keyboard.Key>(RegisteredKeys.Keys);
        foreach (Keyboard.Key key in KeyList)
        {
            if(Keyboard.IsKeyPressed(key))
            {
                Tuple<bool, float> Value = RegisteredKeys[key];
                if (!Value.Item1 && (data.Time.runTime - Value.Item2) > data.Settings.KeyPressDelay)
                {
                    RegisteredKeys[key] = new Tuple<bool, float>(true, data.Time.runTime);
                }

            }
        }
    }
}
