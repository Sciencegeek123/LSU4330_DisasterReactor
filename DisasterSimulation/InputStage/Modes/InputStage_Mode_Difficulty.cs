using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// This file contains the information for painting difficulty onto the simulation.
/// It is largely obsolete.
/// </summary>
partial class InputStage : Stage
{
    void EnterDifficultyState()
    {
        //data.ModesTextList.RemoveAt(0);
        //data.ModesTextList.Insert(0, new System.Tuple<string, bool>("Paint Difficulty", true));

        CursorColor = Color.Blue;

        CurrentInputState = InputStates.Difficulty;

    //    data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
    //    data.Input.TrackKey(Keyboard.Key.R);

    //    data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
    //    data.Input.TrackKey(Keyboard.Key.T);

    //    data.ControlsTextList.Add(Keyboard.Key.Left, new System.Tuple<string, bool>("Left Mouse  - Paint Increase", false));
    //    data.ControlsTextList.Add(Keyboard.Key.Right, new System.Tuple<string, bool>("Right Mouse - Clear Paint", false));
    }

    void ProcessDifficultyState()
    {
        //Update
        DrawCursor();

        //Check Transition
        //if (data.Input.CheckKeyPressed(Keyboard.Key.M))
        //    LeaveDifficultyState();

    }

    void LeaveDifficultyState()
    {
        //Finalize
        //data.Input.UntrackKey(Keyboard.Key.R);
        //data.ControlsTextList.Remove(Keyboard.Key.R);
        //data.Input.UntrackKey(Keyboard.Key.T);
        //data.ControlsTextList.Remove(Keyboard.Key.T);
        //data.Input.UntrackKey(Keyboard.Key.Left);
        //data.ControlsTextList.Remove(Keyboard.Key.Left);
        //data.Input.UntrackKey(Keyboard.Key.Right);
        //data.ControlsTextList.Remove(Keyboard.Key.Right);

        //Transition
        //data.ModesTextList.RemoveAt(0);
        //data.ModesTextList.Insert(0, new System.Tuple<string, bool>("Paint Difficulty", false));
        //EnterDamageState();
    }
}