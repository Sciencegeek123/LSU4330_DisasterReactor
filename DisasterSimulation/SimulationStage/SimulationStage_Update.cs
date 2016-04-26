using DisasterSimulation.OutputStage;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

partial class SimulationStage : Stage
{
    Random rand = new Random();
    
    Sprite SEnv;

    Texture TTra;
    Image ITra;
    Sprite STra;

    bool renderEnv = true;
    bool renderTra = true;

    public override void Update()
    {
        var overlordtime = System.Diagnostics.Stopwatch.StartNew();

        Console.Out.WriteLine("Simulation Update Entered");

        overlord.Update();

        Console.Out.WriteLine("Overlord Update Complete");

        overlordtime.Stop();
        var agenttime = System.Diagnostics.Stopwatch.StartNew();
        foreach (Agent a in data.Agents)
        {
            a.Update();
        }

        Console.Out.WriteLine("Agent Update Complete");
        agenttime.Stop();
        //TODO Update the images;

        if (data.Input.CheckKeyPressed(Keyboard.Key.A))
        {
            data.RenderAgents = !data.RenderAgents;
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.S))
        {
            data.RenderSpawn = !data.RenderSpawn;
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.E))
        {
            renderEnv = !renderEnv;
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.T))
        {
            renderTra = !renderTra;
        }


        if(renderEnv)
        {
            data.Graphics.ProgramDisplayTexture.Draw(SEnv, new RenderStates(BlendMode.Add));
        }

        if(renderTra)
        {

            TTra.Update(data.Trails);

            data.Graphics.ProgramDisplayTexture.Draw(STra, new RenderStates(BlendMode.Add));
        }
        

        data.Graphics.ProgramDisplayTexture.Display();

        print printer = new print();
        List<string> ImageTitles = new List<String>();
        List<string> ImagePaths = new List<String>();

        for(int i = 0; i < 4; i++)
        {
            ImageTitles.Add("C:\\Users\\vneal\\Documents\\GitHub\\LSU4330_DisasterReactor\\DisasterSimulation\\Data\\Bitmap1.bmp");
            ImagePaths.Add("C:\\Users\\vneal\\Documents\\GitHub\\LSU4330_DisasterReactor\\DisasterSimulation\\Data\\Bitmap1.bmp");
        }

        printer.printfunction(ImageTitles,ImagePaths);
        Console.WriteLine("Overlord: " + overlordtime.ElapsedMilliseconds + " AgentTime: " + agenttime.ElapsedMilliseconds);
    }
}
