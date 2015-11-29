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

        data.Input.ClearTrackedKeys();
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

        //Transition
        data.ModesTextList.RemoveAt(7);
        data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", false));
        EnterFinalizeState();
    }
}
