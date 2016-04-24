using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterValueState()
    {
        //data.ModesTextList.RemoveAt(2);
        //data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", true));

        CursorColor = Color.Green;

        CurrentInputState = InputStates.Value;

        //data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        //data.Input.TrackKey(Keyboard.Key.R);

        //data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        //data.Input.TrackKey(Keyboard.Key.T);

        //data.ControlsTextList.Add(Keyboard.Key.Left, new System.Tuple<string, bool>("Left Mouse  - Paint Increase", false));
        //data.ControlsTextList.Add(Keyboard.Key.Right, new System.Tuple<string, bool>("Right Mouse - Clear Paint", false));
    }

    void ProcessValueState()
    {
        //Update
        DrawCursor();

        //Check Transition
        //if (data.Input.CheckKeyPressed(Keyboard.Key.M))
        //    LeaveValueState();
    }

    void LeaveValueState()
    {
        //Cleanup
        //CursorColor = Color.Black;
        //data.Input.UntrackKey(Keyboard.Key.R);
        //data.ControlsTextList.Remove(Keyboard.Key.R);
        //data.Input.UntrackKey(Keyboard.Key.T);
        //data.ControlsTextList.Remove(Keyboard.Key.T);
        //data.Input.UntrackKey(Keyboard.Key.Left);
        //data.ControlsTextList.Remove(Keyboard.Key.Left);
        //data.Input.UntrackKey(Keyboard.Key.Right);
        //data.ControlsTextList.Remove(Keyboard.Key.Right);

        //Transition
        //data.ModesTextList.RemoveAt(2);
        //data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", false));
        //EnterGeographicState();
    }
}