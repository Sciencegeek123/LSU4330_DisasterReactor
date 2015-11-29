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