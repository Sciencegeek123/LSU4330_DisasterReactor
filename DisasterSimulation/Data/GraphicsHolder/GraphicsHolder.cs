using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;
using System;

partial class GraphicsHolder
{
    const float GraphicsInterval = 0.03333f;
    public RenderTexture ProgramDisplayTexture, ProgramInfoTexture;
    public RenderWindow ProgramWindow;

    Button LoadMapButton, RunSimButton;
    RadioButton PaintDifficultyButton, PaintDamageButton, PaintValueButton;
    Panel InspectModePanel, PaintModePanel;

    Data data;

    public void Initialize(Data d)
    {
        data = d;
        
        ProgramDisplayTexture = new RenderTexture(data.Settings.SimulationResolution.X, data.Settings.SimulationResolution.Y);
        ProgramInfoTexture = new RenderTexture(data.Settings.InformationResolution.X, data.Settings.InformationResolution.Y);
        ProgramWindow = new RenderWindow(new VideoMode(data.Settings.ScreenResolution.X, data.Settings.ScreenResolution.Y),"Recovery Simulation - CS3380 Project - wjone48 & sshre18",Styles.Close);
        Button.RenderWindow = ProgramWindow;
        RadioButton.RenderWindow = ProgramWindow;
        ProgramWindow.Closed += onWindowClose;

        ProgramWindow.Clear(Color.White);
        ProgramWindow.Display();
        ProgramWindow.RequestFocus();
        //ProgramWindow.SetMouseCursorVisible(false);

        RegularFont = new Font("Anonymous_Pro.ttf");
        Button.ButtonFont = RegularFont;
        BoldFont = new Font("Anonymous_Pro_B.ttf");

        HeaderText = new Text("Initializing...", RegularFont);
        HeaderText.Color = Color.Black;
        HeaderText.Font = RegularFont;
        HeaderText.CharacterSize = data.Settings.InformationFontSize + 4;
        HeaderText.Position = new Vector2f(5, 5);

        //ModesHeaderText = new Text("Modes: ", RegularFont);
        //INSPECT MODE HEADER TEXT
        ModesHeaderText = new Text("Inspect Mode", RegularFont);
        ModesHeaderText.Color = Color.Black;
        ModesHeaderText.CharacterSize = data.Settings.InformationFontSize + 10;

        //InfoHeaderText = new Text("Info: ", RegularFont);
        //PAINT MODE HEADER TEXT
        InfoHeaderText = new Text("Paint Mode", RegularFont);
        InfoHeaderText.Color = Color.Black;
        InfoHeaderText.CharacterSize = data.Settings.InformationFontSize + 10;

        //ControlsHeaderText = new Text("Controls: ", RegularFont);
        //ControlsHeaderText.Color = Color.Black;
        //ControlsHeaderText.CharacterSize = data.Settings.InformationFontSize + 4;

        TextTemplate = new Text("VOID", RegularFont);
        TextTemplate.Color = Color.Black;
        TextTemplate.CharacterSize = data.Settings.InformationFontSize;

        //Creating Panels
        InspectModePanel = new Panel(new Vector2f(data.Settings.InformationResolution.X * 0.80f, data.Settings.InformationResolution.Y * 0.38f), new Vector2f(data.Settings.InformationResolution.X / 2f, data.Settings.InformationResolution.Y * 0.05f), Panel.PanelModes.InspectMode);
        InspectModePanel.SetActive(true);
        PaintModePanel = new Panel(new Vector2f(data.Settings.InformationResolution.X * 0.80f, data.Settings.InformationResolution.Y * 0.25f), new Vector2f(data.Settings.InformationResolution.X / 2f, data.Settings.InformationResolution.Y * 0.50f), Panel.PanelModes.PaintMode);

        //Creating Buttons
        LoadMapButton = new Button("loadmapimage.png", new Vector2f(data.Graphics.ProgramInfoTexture.Size.X / 5f, data.Graphics.ProgramInfoTexture.Size.Y*0.95f), Button.ButtonFunctions.LoadMap);
        RunSimButton = new Button("runsimimage.png", new Vector2f(4*data.Graphics.ProgramInfoTexture.Size.X / 5f, data.Graphics.ProgramInfoTexture.Size.Y*0.95f), Button.ButtonFunctions.RunSim);

        //Creating Radio Buttons
        PaintDamageButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 125, 620), RadioButton.ButtonFunctions.PaintDamage);
        PaintDamageButton.SelectRadioButton();
        PaintDifficultyButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 125, 668), RadioButton.ButtonFunctions.PaintDifficulty);
        PaintValueButton = new RadioButton(new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 125, 718), RadioButton.ButtonFunctions.PaintValue);
    }

    public void ClearWindow()
    {
        ProgramDisplayTexture.Clear(Color.Black);
        //ProgramInfoTexture.Clear(Color.White);
        ProgramInfoTexture.Clear(new Color(235, 235, 235));
        ProgramWindow.Clear(Color.Magenta);
    }

    Sprite DisplaySprite = new Sprite();
    Sprite InformationSprite = new Sprite();

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

        ProgramWindow.Display();
    }
}

