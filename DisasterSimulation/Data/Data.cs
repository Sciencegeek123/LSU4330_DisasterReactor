using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

class Data
{
    public SettingHolder Settings;
    public GraphicsHolder Graphics;
    public TimeHolder Time;

    public List<Tuple<string, bool>> ModesTextList = new List<Tuple<string, bool>>();
    public List<Tuple<string, bool>> InfoTextList = new List<Tuple<string, bool>>();
    public List<Tuple<string, bool>> ControlsTextList = new List<Tuple<string, bool>>();

    public Image Environment;

    public void Initialize()
    {
        Time = new TimeHolder();
        Graphics = new GraphicsHolder();
        Settings = new SettingHolder();

        Time.Initialize();
        Graphics.Initialize(this);
    }

    public void PreUpdate()
    {
        Time.Update();
        Graphics.ClearWindow();
    }

    public void PostUpdate()
    {
        Graphics.RenderInfo(this);
        Graphics.RenderWindow(this);
    }
}

