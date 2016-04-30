using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// This file contains the information for preparing the simulation for the geography state.
/// It is completely obsolete.
/// </summary>
partial class InputStage : Stage
{
    void EnterGeographicState()
    {
        //data.ModesTextList.RemoveAt(4);
        //data.ModesTextList.Insert(4, new System.Tuple<string, bool>("Geographic Input", true));

        CurrentInputState = InputStates.Geographic;

        //data.ControlsTextList.Add(Keyboard.Key.L,new System.Tuple<string, bool>("L - Load from File", false));
        //data.Input.TrackKey(Keyboard.Key.L);
    }

    void ProcessGeographicState()
    {
        //Update

        //Check Transition
        //if (data.Input.CheckKeyPressed(Keyboard.Key.M))
        //    LeaveGeographicState();

    }

    void LeaveGeographicState()
    {
        //Cleanup
        data.ControlsTextList.Remove(Keyboard.Key.L);
        data.Input.UntrackKey(Keyboard.Key.L);

        //Transition
        //data.ModesTextList.RemoveAt(4);
        //data.ModesTextList.Insert(4, new System.Tuple<string, bool>("Geographic Input", false));
        EnterInfastructureState();
    }
}