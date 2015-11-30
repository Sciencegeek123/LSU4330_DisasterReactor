using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{

    void EnterInspectState()
    {
        data.ModesTextList.RemoveAt(7);
        data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", true));

        CurrentInputState = InputStates.Inspect;

        data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.R);

        data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.T);
    }

    void ProcessInspectState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveInspectState();

    }

    void LeaveInspectState()
    {
        //Cleanup
        data.Input.UntrackKey(Keyboard.Key.R);
        data.ControlsTextList.Remove(Keyboard.Key.R);
        data.Input.UntrackKey(Keyboard.Key.T);
        data.ControlsTextList.Remove(Keyboard.Key.T);

        //Transition
        data.ModesTextList.RemoveAt(7);
        data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", false));
        EnterFinalizeState();
    }
}
