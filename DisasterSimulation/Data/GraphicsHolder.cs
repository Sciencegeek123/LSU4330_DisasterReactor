using SFML.Graphics;
using SFML.System;
using SFML.Window;

class GraphicsHolder
{
    const float GraphicsInterval = 0.03333f;
    public RenderTexture ProgramDisplayTexture, ProgramInfoTexture;
    RenderWindow ProgramWindow;


    public void Initialize(SettingHolder settings)
    {
        ProgramDisplayTexture = new RenderTexture(settings.SimulationResolution.X, settings.SimulationResolution.Y);
        ProgramInfoTexture = new RenderTexture(settings.InformationResolution.X, settings.InformationResolution.Y);
        ProgramWindow = new RenderWindow(new VideoMode(settings.ScreenResolution.X, settings.ScreenResolution.Y),"Recovery Simulation - CS3380 Project - wjone48 & sshre18");
    }

    public void ClearWindow()
    {
        ProgramDisplayTexture.Clear(Color.Red);
        ProgramInfoTexture.Clear(Color.Blue);
        ProgramWindow.Clear(Color.White);
    }

    public void RenderWindow(Data data)
    {
        ProgramDisplayTexture.Display();
        ProgramInfoTexture.Display();

        Sprite DisplaySprite = new Sprite(ProgramDisplayTexture.Texture);
        Sprite InformationSprite = new Sprite(ProgramInfoTexture.Texture);

        DisplaySprite.Position = data.Settings.SimulationPosition;
        DisplaySprite.Scale = data.Settings.SimulationScale;

        InformationSprite.Position = data.Settings.InformationPosition;
        InformationSprite.Scale = data.Settings.InformationScale;

        ProgramWindow.Draw(DisplaySprite);
        ProgramWindow.Draw(InformationSprite);

        ProgramWindow.Display();
    }


}

