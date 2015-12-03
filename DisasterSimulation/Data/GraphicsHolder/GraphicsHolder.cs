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

    Data data;

    public void Initialize(Data d)
    {
        data = d;

        ProgramDisplayTexture = new RenderTexture(data.Settings.SimulationResolution.X, data.Settings.SimulationResolution.Y);
        ProgramInfoTexture = new RenderTexture(data.Settings.InformationResolution.X, data.Settings.InformationResolution.Y);
        ProgramWindow = new RenderWindow(new VideoMode(data.Settings.ScreenResolution.X, data.Settings.ScreenResolution.Y),"Recovery Simulation - CS3380 Project - wjone48 & sshre18",Styles.Close);

        ProgramWindow.Closed += onWindowClose;

        ProgramWindow.Clear(Color.White);
        ProgramWindow.Display();
        ProgramWindow.RequestFocus();
        ProgramWindow.SetMouseCursorVisible(false);

        RegularFont = new Font("Anonymous_Pro.ttf");
        BoldFont = new Font("Anonymous_Pro_B.ttf");

        HeaderText = new Text("Initializing...", RegularFont);
        HeaderText.Color = Color.Black;
        HeaderText.Font = RegularFont;
        HeaderText.CharacterSize = data.Settings.InformationFontSize + 4;
        HeaderText.Position = new Vector2f(5, 5);

        ModesHeaderText = new Text("Modes: ", RegularFont);
        ModesHeaderText.Color = Color.Black;
        ModesHeaderText.CharacterSize = data.Settings.InformationFontSize + 4;

        InfoHeaderText = new Text("Info: ", RegularFont);
        InfoHeaderText.Color = Color.Black;
        InfoHeaderText.CharacterSize = data.Settings.InformationFontSize + 4;

        ControlsHeaderText = new Text("Controls: ", RegularFont);
        ControlsHeaderText.Color = Color.Black;
        ControlsHeaderText.CharacterSize = data.Settings.InformationFontSize + 4;

        TextTemplate = new Text("VOID", RegularFont);
        TextTemplate.Color = Color.Black;
        TextTemplate.CharacterSize = data.Settings.InformationFontSize;

    }

    public void ClearWindow()
    {
        ProgramDisplayTexture.Clear(Color.Black);
        ProgramInfoTexture.Clear(Color.White);
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

        foreach (Vector2f spawn in data.SpawnPositions) {
            template.Position = spawn;
            ProgramDisplayTexture.Draw(template);
        }

        template.OutlineColor = Color.Black;

        foreach (Agent a in data.Agents)
        {
            template.Position = a.Position;
            template.FillColor = a.data;
            ProgramDisplayTexture.Draw(template);
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

        ProgramWindow.Display();
    }


}

