using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

/// <summary>
/// This file contains the information for preparing the simulation for the simulation state.
/// It is largely obsolete.
/// </summary>
partial class InputStage : Stage
{

    void EnterFinalizeState()
    {
        //data.ModesTextList.RemoveAt(8);
        //data.ModesTextList.Insert(8, new System.Tuple<string, bool>("Finalize", true));

        CurrentInputState = InputStates.Finalize;

        //data.ControlsTextList.Add(Keyboard.Key.Return,new System.Tuple<string, bool>("Return - Begin Simulation", false));
        //data.Input.TrackKey(Keyboard.Key.Return);
    }

    bool SpawnWarning = true;

    void ProcessFinalizeState()
    {

    }

    void LeaveFinalizeState()
    {
        //Cleanup
        data.Input.UntrackKey(Keyboard.Key.Return);
        data.ControlsTextList.Remove(Keyboard.Key.Return);
        EnterDifficultyState();
    }
}