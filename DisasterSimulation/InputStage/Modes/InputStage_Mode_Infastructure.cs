﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// This file contains the information for preparing the simulation for the infastructure state.
/// It is completely obsolete.
/// </summary>
partial class InputStage : Stage
{
    void EnterInfastructureState()
    {
        //data.ModesTextList.RemoveAt(5);
        //data.ModesTextList.Insert(5, new System.Tuple<string, bool>("Infastructure Input", true));

        CurrentInputState = InputStates.Infastructure;

        //data.ControlsTextList.Add(Keyboard.Key.L, new System.Tuple<string, bool>("L - Load from File", false));
        //data.Input.TrackKey(Keyboard.Key.L);
    }

    void ProcessInfastructureState()
    {
        //Update
        if (data.Input.CheckKeyPressed(Keyboard.Key.L))
        {
            string FileName;
            if (GetFileFromBrowser(out FileName))
            {
                InfastructureHolder InfLoadHolder = new InfastructureHolder();
                InfLoadHolder.parseFile(FileName);
                if (InfLoadHolder.isValid())
                {
                    Sprite env = new Sprite(InfLoadHolder.exportTextureResults());

                    env.Origin = new Vector2f(512, 512);
                    env.Position = new Vector2f(512, 512);
                    env.Rotation = -90;

                    EnvironmentProduction.Draw(env);

                }
            }
        }

        //Check Transition
        //if (data.Input.CheckKeyPressed(Keyboard.Key.M))
        //    LeaveInfastructureState();
    }

    void LeaveInfastructureState()
    {
        //Cleanup
        data.ControlsTextList.Remove(Keyboard.Key.L);
        data.Input.UntrackKey(Keyboard.Key.L);

        //Transition
        //data.ModesTextList.RemoveAt(5);
        //data.ModesTextList.Insert(5, new System.Tuple<string, bool>("Infastructure Input", false));
        EnterInspectState();
    }
}