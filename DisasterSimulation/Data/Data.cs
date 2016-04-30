using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

/// <summary>
/// The primary data structure for the application. Contains all of the references and infromation that gets passed from stage to stage.
/// </summary>
class Data
{
    public SettingHolder Settings;
    public GraphicsHolder Graphics;
    public TimeHolder Time;
    public InputHolder Input;

    public List<Tuple<string, bool>> ModesTextList = new List<Tuple<string, bool>>();
    public List<Tuple<string, bool>> InfoTextList = new List<Tuple<string, bool>>();
    public SortedList<Keyboard.Key, Tuple<string, bool>> ControlsTextList = new SortedList<Keyboard.Key, Tuple<string, bool>>();
    public List<Vector2f> SpawnPositions = new List<Vector2f>();
    public List<Agent> Agents = new List<Agent>();

    public int[,] PositionHeatmap;
    public int[,] AidHeatmap;
    public int[,] RepairHeatmap;

    public Image Environment;
    public byte[] Trails = new byte[1024 * 1024 * 4];

    public bool RenderAgents = true;
    public bool RenderSpawn = true;

    public Random rand = new Random();

    private float LastRenderTime = -1;
    
    /// <summary>
    /// Returns a pixel from the Trails image.
    /// </summary>
    /// <param name="X">The X location.</param>
    /// <param name="Y">The Y location.</param>
    /// <returns>The color at the given location.</returns>
    public Color getPixel(int X, int Y)
    {
        return new Color(Trails[(Y * 1024 + X) * 4 + 0], Trails[(Y * 1024 + X) * 4 + 1], Trails[(Y * 1024 + X) * 4 + 2], Trails[(Y * 1024 + X) * 4 + 3]);
    }

    /// <summary>
    /// Sets a pixel in the Trails image.
    /// </summary>
    /// <param name="X">The X location.</param>
    /// <param name="Y">The Y location.</param>
    /// <param name="C">The color to set.</param>
    public void setPixel(int X, int Y, Color C)
    {
        int pos = (Y * 1024 + X) * 4;

        if (pos > 1024 * 1024 * 4)
            Console.WriteLine("P:" + pos + " X:" + X + " Y:" + Y + "C:" + C);

        Trails[pos + 0] = C.R;
        Trails[pos + 1] = C.G;
        Trails[pos + 2] = C.B;
    }

    /// <summary>
    /// Initializes the primary structure and sub-components.
    /// </summary>
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

    /// <summary>
    /// Prepares the data structure for the next iteration.
    /// </summary>
    public void PreUpdate()
    {
        Graphics.ProgramWindow.DispatchEvents();
        Time.Update();
        Graphics.ClearWindow();
        Input.Update();
    }

    /// <summary>
    /// Completes all necessary actions before the next iteration.
    /// </summary>
    public void PostUpdate()
    {
        Graphics.RenderInfo();
        Graphics.RenderWindow();
        LastRenderTime = Time.runTime;
    }
}

