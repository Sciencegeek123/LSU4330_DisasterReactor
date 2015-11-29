using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Data
{
    public SettingHolder Settings;
    public GraphicsHolder Graphics;
    public TimeHolder Time;

   public Image Environment;

    public void Initialize()
    {
        Time = new TimeHolder();
        Graphics = new GraphicsHolder();
        Settings = new SettingHolder();

        Graphics.Initialize(Settings);
    }

    public void PreUpdate()
    {
        Graphics.ClearWindow();
    }

    public void PostUpdate()
    {
        Graphics.RenderWindow(this);
    }
}

