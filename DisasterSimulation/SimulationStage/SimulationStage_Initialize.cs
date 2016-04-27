﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class SimulationStage : Stage
{
    int agentCount = 512;
    Button PrintPdfButton, ExitButton;

    public override void Initialize(Data d)
    {

        data = d;
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Simulation Stage", false));
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Press ESC to exit the program when desired.", false));

        data.ControlsTextList.Add(Keyboard.Key.A, new Tuple<string, bool>("Toggle Agents", false));
        //data.Input.TrackKey(Keyboard.Key.A);

        data.ControlsTextList.Add(Keyboard.Key.S, new Tuple<string, bool>("Toggle Spawns", false));
        //data.Input.TrackKey(Keyboard.Key.S);

        data.ControlsTextList.Add(Keyboard.Key.E, new Tuple<string, bool>("Toggle Environment", false));
        //data.Input.TrackKey(Keyboard.Key.E);

        data.ControlsTextList.Add(Keyboard.Key.T, new Tuple<string, bool>("Toggle Trails", false));
        //data.Input.TrackKey(Keyboard.Key.T);

        for (int i = 0; i < 4096 * 4096 * 4; i++)
        {
            if (i % 4 == 3)
                data.Trails[i] = 255;
            else
                data.Trails[i] = 0;

        }


        ITra = new Image(4096, 4096, data.Trails);

        TTra = new Texture(ITra);

        STra = new Sprite(TTra);

        SEnv = new Sprite(new Texture(data.Environment));
        
        int offset = 0;
        if (data.SpawnPositions.Count != 0) {
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.init(data,data.SpawnPositions[offset]);
                a.info = new Color(255, 255, 0);
                offset = ++offset % data.SpawnPositions.Count;
                data.Agents.Add(a);
                
            }
        } else
        {
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.init(data,new Vector2f(2048, 2048));
                a.info = new Color(32, 32, 0);
                offset = ++offset % data.SpawnPositions.Count;
                data.Agents.Add(a);

            }
        }

        PrintPdfButton = new Button("printimage.png", new Vector2f(1*data.Graphics.ProgramInfoTexture.Size.X / 4f, data.Graphics.ProgramInfoTexture.Size.Y * 0.95f), Button.ButtonFunctions.PrintPDF);
        ExitButton = new Button("exitimage.png", new Vector2f(3 * data.Graphics.ProgramInfoTexture.Size.X / 4f, data.Graphics.ProgramInfoTexture.Size.Y * 0.95f), Button.ButtonFunctions.ExitSim);
    }
}
