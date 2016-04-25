﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class SimulationStage : Stage
{
    int agentCount = 512;

    public override void Initialize(Data d)
    {
        data = d;
        data.InfoTextList.Add(new System.Tuple<string, bool>("Simulation Stage", true));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Press ESC to exit the program when desired.", true));

        data.ControlsTextList.Add(Keyboard.Key.A, new Tuple<string, bool>("A - Toggle Agents", false));
        data.Input.TrackKey(Keyboard.Key.A);

        data.ControlsTextList.Add(Keyboard.Key.S, new Tuple<string, bool>("S - Toggle Spawns", false));
        data.Input.TrackKey(Keyboard.Key.S);

        data.ControlsTextList.Add(Keyboard.Key.E, new Tuple<string, bool>("E - Toggle Environment", false));
        data.Input.TrackKey(Keyboard.Key.E);

        data.ControlsTextList.Add(Keyboard.Key.T, new Tuple<string, bool>("T - Toggle Trails", false));
        data.Input.TrackKey(Keyboard.Key.T);

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

        overlord = new Overlord();

        overlord.Initialize(data);

        Vector2f halfSize = new Vector2f(data.Environment.Size.X / 2, data.Environment.Size.Y / 2);

        if (data.SpawnPositions.Count > 0) { //Use the spawn points if there are some.
            int offset = 0;
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.init(data,data.SpawnPositions[offset], overlord);
                a.info = new Color(255, 255, 0);
                offset = ++offset % data.SpawnPositions.Count;
                data.Agents.Add(a);
                
            }
        } else //Spawn them in the middle of the map.
        {
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.init(data, halfSize, overlord);
                a.info = new Color(32, 32, 0);
                data.Agents.Add(a);

            }
        }
    }
}
