using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterGeographicState()
    {
        data.ModesTextList.RemoveAt(4);
        data.ModesTextList.Insert(4, new System.Tuple<string, bool>("Geographic Input", true));

        CurrentInputState = InputStates.Geographic;
    }

    void ProcessGeographicState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveGeographicState();

    }

    void LeaveGeographicState()
    {
        //Cleanup

        //Transition
        data.ModesTextList.RemoveAt(4);
        data.ModesTextList.Insert(4, new System.Tuple<string, bool>("Geographic Input", false));
        EnterInfastructureState();
    }
}