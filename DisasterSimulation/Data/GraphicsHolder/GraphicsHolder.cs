﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;
using System;

/// <summary>
/// The graphics holder class contains all of the data structures and functions needed to render the application.
/// </summary>
partial class GraphicsHolder
{
    const float GraphicsInterval = 0.03333f;
    public RenderTexture ProgramDisplayTexture, ProgramInfoTexture;
    public RenderWindow ProgramWindow;

    Button LoadMapButton, RunSimButton;
    RadioButton PaintDifficultyButton, PaintDamageButton, PaintValueButton, InspectModeButton;
    ToggleButton ToggleAgentsButton, ToggleSpawnsButton, ToggleEnvironmentButton, ToggleTrailsButton;
    Panel InspectModePanel, PaintModePanel;

    Data data;

    /// <summary>
    /// The initialize function will setup all of the elements of the project. Namely SFML components.
    /// </summary>
    /// <param name="d">A reference to the primary data structure in the application.</param>
    public void Initialize(Data d)
    {
        data = d;
        
        ProgramDisplayTexture = new RenderTexture(data.Settings.SimulationResolution.X, data.Settings.SimulationResolution.Y);
        ProgramInfoTexture = new RenderTexture(data.Settings.InformationResolution.X, data.Settings.InformationResolution.Y);
        ProgramWindow = new RenderWindow(new VideoMode(data.Settings.ScreenResolution.X, data.Settings.ScreenResolution.Y),"Disaster Reactor - CS4330 Project",Styles.Close | Styles.Titlebar);
        
        Button.RenderWindow = ProgramWindow;
        RadioButton.RenderWindow = ProgramWindow;
        ToggleButton.RenderWindow = ProgramWindow;
        ProgramWindow.Closed += onWindowClose;

        ProgramWindow.Clear(Color.White);
        ProgramWindow.Display();
        ProgramWindow.RequestFocus();

        RegularFont = new Font("Anonymous_Pro.ttf");
        Button.ButtonFont = RegularFont;
        BoldFont = new Font("Anonymous_Pro_B.ttf");

        HeaderText = new Text("Initializing...", RegularFont);
        HeaderText.Color = Color.Black;
        HeaderText.Font = RegularFont;
        HeaderText.CharacterSize = data.Settings.InformationFontSize + 4;
        HeaderText.Position = new Vector2f(5, 5);

        //INSPECT MODE HEADER TEXT
        ModesHeaderText = new Text("Inspect Mode", RegularFont);
        ModesHeaderText.Color = Color.Black;
        ModesHeaderText.CharacterSize = data.Settings.InformationFontSize + 10;

        //PAINT MODE HEADER TEXT
        InfoHeaderText = new Text("Mode", RegularFont);
        InfoHeaderText.Color = Color.Black;
        InfoHeaderText.CharacterSize = data.Settings.InformationFontSize + 10;
        InfoHeaderText.Origin = new Vector2f(InfoHeaderText.GetLocalBounds().Width / 2f, InfoHeaderText.GetLocalBounds().Height / 2f);

        TextTemplate = new Text("VOID", RegularFont);
        TextTemplate.Color = Color.Black;
        TextTemplate.CharacterSize = data.Settings.InformationFontSize;

        //Creating Panels
        InspectModePanel = new Panel(new Vector2f(data.Settings.InformationResolution.X * 0.85f, data.Settings.InformationResolution.Y * 0.38f), new Vector2f(data.Settings.InformationResolution.X / 2f, data.Settings.InformationResolution.Y * 0.37f), Panel.PanelModes.InspectMode);
        PaintModePanel = new Panel(new Vector2f(data.Settings.InformationResolution.X * 0.85f, data.Settings.InformationResolution.Y * 0.30f), new Vector2f(data.Settings.InformationResolution.X / 2f, data.Settings.InformationResolution.Y * 0.05f), Panel.PanelModes.PaintMode);

        //Creating Buttons
        LoadMapButton = new Button("loadmapimage.png", new Vector2f(data.Graphics.ProgramInfoTexture.Size.X / 5f, data.Graphics.ProgramInfoTexture.Size.Y*0.95f), Button.ButtonFunctions.LoadMap);
        RunSimButton = new Button("runsimimage.png", new Vector2f(4*data.Graphics.ProgramInfoTexture.Size.X / 5f, data.Graphics.ProgramInfoTexture.Size.Y*0.95f), Button.ButtonFunctions.RunSim);
        
        //Creating Radio Buttons
        InspectModeButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 150, 160), RadioButton.ButtonFunctions.Inspect);
        PaintDamageButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 150, 210), RadioButton.ButtonFunctions.PaintDamage);
        PaintDifficultyButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 150, 260), RadioButton.ButtonFunctions.PaintDifficulty);
        PaintValueButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 150, 310), RadioButton.ButtonFunctions.PaintValue);


        //Creating Toggle Buttons
        ToggleAgentsButton = new ToggleButton(new Vector2f(80, 182), ToggleButton.ToggleOptions.ToggleAgents);
        ToggleAgentsButton.ChangeToggleStatus(true);
        ToggleEnvironmentButton = new ToggleButton(new Vector2f(80, 230), ToggleButton.ToggleOptions.ToggleEnvironment);
        ToggleEnvironmentButton.ChangeToggleStatus(true);
        ToggleSpawnsButton = new ToggleButton(new Vector2f(80, 278), ToggleButton.ToggleOptions.ToggleSpawns);
        ToggleSpawnsButton.ChangeToggleStatus(true);
        ToggleTrailsButton = new ToggleButton(new Vector2f(80, 326), ToggleButton.ToggleOptions.ToggleTrails);
        ToggleTrailsButton.ChangeToggleStatus(true);
    }

    /// <summary>
    /// Clears the window and render textures for the Application.
    /// </summary>
    public void ClearWindow()
    {
        ProgramDisplayTexture.Clear(Color.Black);
        ProgramInfoTexture.Clear(new Color(235, 235, 235));
        ProgramWindow.Clear(Color.Magenta);
    }

    Sprite DisplaySprite = new Sprite();
    Sprite InformationSprite = new Sprite();

    /// <summary>
    /// Renders the primary window after the render textures have been populated.
    /// </summary>
    public void RenderWindow()
    {

        CircleShape template = new CircleShape();
        template.FillColor = Color.Black;
        template.OutlineColor = Color.White;
        template.OutlineThickness = 4;
        template.Radius = 8;
        if (data.RenderSpawn)
        {
            foreach (Vector2f spawn in data.SpawnPositions)
            {
                template.Position = spawn;
                ProgramDisplayTexture.Draw(template);
            }
        }

        template.OutlineColor = Color.Black;

        if (data.RenderAgents)
        {
            foreach (Agent a in data.Agents)
            {
                template.Position = new Vector2f(a.Position.X, a.Position.Y);
                template.FillColor = a.info;
                ProgramDisplayTexture.Draw(template);
            }
        }


        ProgramDisplayTexture.Display();
        ProgramInfoTexture.Display();

        DisplaySprite.Texture = ProgramDisplayTexture.Texture;
        InformationSprite.Texture = ProgramInfoTexture.Texture;

        DisplaySprite.Position = data.Settings.SimulationPosition;
        DisplaySprite.Scale = data.Settings.SimulationScale;

        InformationSprite.Position = data.Settings.InformationPosition;
        InformationSprite.Scale = data.Settings.InformationScale;

        ProgramWindow.Draw(DisplaySprite);
        ProgramWindow.Draw(InformationSprite);

        foreach (Button current in Button.ButtonList)
        {
            ProgramWindow.Draw(current.ButtonSprite);
        }
        foreach(RadioButton current in RadioButton.RadioButtonList)
        {
            ProgramWindow.Draw(current.ButtonShape_Outer);
            ProgramWindow.Draw(current.ButtonShape_Inner);
        }
        if(SimStageLoaded)
        {
            foreach(ToggleButton current in ToggleButton.ToggleButtonList)
            {
                ProgramWindow.Draw(current.OuterShape);
                ProgramWindow.Draw(current.InnerShape);
            }
        }

        ProgramWindow.Display();
    }
}

