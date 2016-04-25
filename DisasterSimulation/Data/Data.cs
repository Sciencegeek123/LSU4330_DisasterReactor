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
    public SortedDictionary<Keyboard.Key, Tuple<string, bool>> ControlsTextList = new SortedDictionary<Keyboard.Key, Tuple<string, bool>>();
    public List<Vector2f> SpawnPositions = new List<Vector2f>();
    public List<Agent> Agents = new List<Agent>();

    public Image Environment;
    public byte[] Trails = new byte[4096 * 4096 * 4];

    public bool RenderAgents = true;
    public bool RenderSpawn = true;

    public Random rand = new Random();

    private float LastRenderTime = -1;

    public Color getPixel(uint X, uint Y)
    {
        //Console.WriteLine("Data.cs: X:" + X + " Y:" + Y);
        return new Color(Trails[(Y * 4096 + X) * 4 + 0], Trails[(Y * 4096 + X) * 4 + 1], Trails[(Y * 4096 + X) * 4 + 2], Trails[(Y * 4096 + X) * 4 + 3]);
    }

    public void setPixel(int X, int Y, Color C)
    {
        int pos = (Y * 4096 + X) * 4;

        if (pos > 4096 * 4096 * 4)
            Console.WriteLine("P:" + pos + " X:" + X + " Y:" + Y + "C:" + C);

        Trails[pos + 0] = C.R;
        Trails[pos + 1] = C.G;
        Trails[pos + 2] = C.B;
    }

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
        LastRenderTime = Time.runTime;
    }
}

