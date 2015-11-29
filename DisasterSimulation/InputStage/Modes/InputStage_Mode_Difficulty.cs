using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterDifficultyState()
    {
        data.ModesTextList.RemoveAt(0);
        data.ModesTextList.Insert(0, new System.Tuple<string, bool>("Paint Difficulty", true));

        CursorColor = Color.Blue;

        CurrentInputState = InputStates.Difficulty;

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

    void ProcessDifficultyState()
    {
        //Update
        DrawCursor();

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveDifficultyState();

    }

    void LeaveDifficultyState()
    {
        //Finalize

        //Transition
        data.ModesTextList.RemoveAt(0);
        data.ModesTextList.Insert(0, new System.Tuple<string, bool>("Paint Difficulty", false));
        EnterDamageState();
    }
}