using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Data
{
    public SettingHolder Settings;
    public GraphicsHolder Graphics;
    public TimeHolder Time;

   public Image SimulationEnvironment;

    public void Initialize()
    {
        Time = new TimeHolder();
        Graphics = new GraphicsHolder();
        Settings = new SettingHolder();

        SimulationEnvironment = new Image(Settings.SimulationResolution.X, Settings.SimulationResolution.Y, Color.Black);

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

