﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class InputStage : Stage
{

    void EnterFinalizeState()
    {
        data.ModesTextList.RemoveAt(8);
        data.ModesTextList.Insert(8, new System.Tuple<string, bool>("Finalize", true));

        CurrentInputState = InputStates.Finalize;

        data.ControlsTextList.Add(Keyboard.Key.Return,new System.Tuple<string, bool>("Return - Begin Simulation", false));
        data.Input.TrackKey(Keyboard.Key.Return);
    }

    bool SpawnWarning = true;

    void ProcessFinalizeState()
    {
        //Update
        if(data.Input.CheckKeyPressed(Keyboard.Key.Return))
        {
            if(data.SpawnPositions.Count == 0)
            {
                if (SpawnWarning)
                {
                    SpawnWarning = false;
                    data.InfoTextList.Add(new Tuple<string, bool>("At least one spawn must be set.", true));
                }
            } else
            {
                EnvironmentProduction.Display();
                data.Environment = EnvironmentProduction.Texture.CopyToImage();
                PerformStageTransition = true;
                return;
            }
        }

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveFinalizeState();

    }

    void LeaveFinalizeState()
    {
        //Cleanup
        data.Input.UntrackKey(Keyboard.Key.Return);
        data.ControlsTextList.Remove(Keyboard.Key.Return);


        //Transition
        data.ModesTextList.RemoveAt(8);
        data.ModesTextList.Insert(8, new System.Tuple<string, bool>("Finalize", false));
        EnterDifficultyState();
    }
}