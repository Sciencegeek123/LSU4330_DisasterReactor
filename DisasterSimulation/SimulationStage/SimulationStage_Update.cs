using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


partial class SimulationStage : Stage
{
    Random rand = new Random();
    
    Sprite SEnv;

    Texture TTra;
    Image ITra;
    Sprite STra;

    public override void Update()
    {

        foreach(Agent a in data.Agents)
        {
            a.Update();
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.A))
        {
            data.RenderAgents = !data.RenderAgents;
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.S))
        {
            data.RenderSpawn = !data.RenderSpawn;
        }

        TTra.Update(data.Trails);

        data.Graphics.ProgramDisplayTexture.Draw(SEnv, new RenderStates(BlendMode.Add));
        data.Graphics.ProgramDisplayTexture.Draw(STra, new RenderStates(BlendMode.Add));

        data.Graphics.ProgramDisplayTexture.Display();
    }
}
