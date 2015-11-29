using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterInfastructureState()
    {
        data.ModesTextList.RemoveAt(5);
        data.ModesTextList.Insert(5, new System.Tuple<string, bool>("Infastructure Input", true));

        CurrentInputState = InputStates.Infastructure;
    }

    void ProcessInfastructureState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveInfastructureState();
    }

    void LeaveInfastructureState()
    {
        //Cleanup

        //Transition
        data.ModesTextList.RemoveAt(5);
        data.ModesTextList.Insert(5, new System.Tuple<string, bool>("Infastructure Input", false));
        EnterInspectState();
    }
}