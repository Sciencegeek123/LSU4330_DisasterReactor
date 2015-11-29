using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{

    void EnterFinalizeState()
    {
        data.ModesTextList.RemoveAt(8);
        data.ModesTextList.Insert(8, new System.Tuple<string, bool>("Finalize", true));

        CurrentInputState = InputStates.Finalize;
    }

    void ProcessFinalizeState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveFinalizeState();

    }

    void LeaveFinalizeState()
    {
        //Cleanup

        //Transition
        data.ModesTextList.RemoveAt(8);
        data.ModesTextList.Insert(8, new System.Tuple<string, bool>("Finalize", false));
        EnterDifficultyState();
    }
}