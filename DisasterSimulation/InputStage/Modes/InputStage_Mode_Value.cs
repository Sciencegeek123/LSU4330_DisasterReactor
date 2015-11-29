using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterValueState()
    {
        data.ModesTextList.RemoveAt(2);
        data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", true));

        CursorColor = Color.Green;

        CurrentInputState = InputStates.Value;

        data.Input.ClearTrackedKeys();

        data.ControlsTextList.Add(new System.Tuple<string, bool>("M - Switch Modes", false));
        data.Input.TrackKey(Keyboard.Key.M);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.R);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.T);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("Left Mouse  - Paint Increase", false));
        data.ControlsTextList.Add(new System.Tuple<string, bool>("Right Mouse - Clear Paint", false));
    }

    void ProcessValueState()
    {
        //Update
        DrawCursor();

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveValueState();
    }

    void LeaveValueState()
    {
        //Cleanup
        CursorColor = Color.Black;

        //Transition
        data.ModesTextList.RemoveAt(2);
        data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", false));
        EnterGeographicState();
    }
}