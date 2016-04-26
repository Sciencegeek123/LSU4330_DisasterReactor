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
}