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
    public InputHolder Input;

    public List<Tuple<string, bool>> ModesTextList = new List<Tuple<string, bool>>();
    public List<Tuple<string, bool>> InfoTextList = new List<Tuple<string, bool>>();
    public List<Tuple<string, bool>> ControlsTextList = new List<Tuple<string, bool>>();

    public Image Environment;

    public void Initialize()
    {
        Time = new TimeHolder();
        Graphics = new GraphicsHolder();
        Settings = new SettingHolder();
        Input = new InputHolder();

        Input.Initialize(this);
        Time.Initialize();
        Graphics.Initialize(this);
    }

    public void PreUpdate()
    {
        Graphics.ProgramWindow.DispatchEvents();
        Time.Update();
        Graphics.ClearWindow();
        Input.Update();
    }

    public void PostUpdate()
    {
        Graphics.RenderInfo();
        Graphics.RenderWindow();
    }
}

