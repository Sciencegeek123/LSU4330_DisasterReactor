using DisasterSimulation.OutputStage;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

using System.Collections.Generic;
using System.Windows.Forms;

partial class SimulationStage : Stage
{
    Random rand = new Random();
    
    Sprite SEnv;

    Texture TTra;
    Image ITra;
    Sprite STra;

    bool renderEnv = true;
    bool renderTra = true;

    float mouseClickDelay = 0.25f;
    bool mouseClickUsable;

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
        //TODO Update the images;        }


        if(!mouseClickUsable) // ugly placement for custom mouse click delay

        {
            mouseClickDelay -= data.Time.deltaTime;
            if(mouseClickDelay <= 0)
            {
                mouseClickUsable = true;
                mouseClickDelay = 0.25f;
            }
        }

        if(Mouse.IsButtonPressed(Mouse.Button.Left) && mouseClickUsable) // check for toggle or print button clicks
        {
            mouseClickUsable = false;
            ToggleButton ClickedToggleButton = ToggleButton.GetToggleButtonClicked();
            if(ClickedToggleButton != null)
            {
                ClickedToggleButton.ChangeToggleStatus(!ClickedToggleButton.IsToggled);
                switch(ClickedToggleButton.ToggleOption)
                {
                    case ToggleButton.ToggleOptions.ToggleAgents:
                        {
                            data.RenderAgents = ClickedToggleButton.IsToggled;
                            break;
                        }

                    case ToggleButton.ToggleOptions.ToggleEnvironment:
                        {
                            renderEnv = ClickedToggleButton.IsToggled;
                            break;
                        }

                    case ToggleButton.ToggleOptions.ToggleSpawns:
                        {
                            data.RenderSpawn = ClickedToggleButton.IsToggled;
                            break;
                        }

                    case ToggleButton.ToggleOptions.ToggleTrails:
                        {
                            renderTra = ClickedToggleButton.IsToggled;
                            break;
                        }
                }
            }
            else
            {
                Button ClickedButton = Button.GetButtonClicked();
                if(ClickedButton != null)
                {
                    if(ClickedButton.Function == Button.ButtonFunctions.PrintPDF)
                    {
                        string thePath;
                        FolderBrowserDialog FBD = new FolderBrowserDialog();
                        if (FBD.ShowDialog() == DialogResult.OK)
                        {
                            thePath = FBD.SelectedPath;
                        }
                        else
                        {
                            thePath = "NULL";
                        }
                        PrintResults(thePath);
                    }
                }
            }
        }

        //if(data.Input.CheckKeyPressed(Keyboard.Key.A))
        //{
        //    data.RenderAgents = !data.RenderAgents;
        //}

        //if(data.Input.CheckKeyPressed(Keyboard.Key.S))
        //{
        //    data.RenderSpawn = !data.RenderSpawn;
        //}

        //if(data.Input.CheckKeyPressed(Keyboard.Key.E))
        //{
        //    renderEnv = !renderEnv;
        //}

        //if(data.Input.CheckKeyPressed(Keyboard.Key.T))
        //{
        //    renderTra = !renderTra;
        //}


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


        Console.WriteLine("Overlord: " + overlordtime.ElapsedMilliseconds + " AgentTime: " + agenttime.ElapsedMilliseconds);
    }
}
