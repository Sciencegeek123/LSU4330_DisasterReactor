using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterValueState()
    {
        data.ModesTextList.RemoveAt(2);
        data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", true));

        CurrentInputState = InputStates.Value;
    }

    void ProcessValueState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveValueState();
    }

    void LeaveValueState()
    {
        //Cleanup

        //Transition
        data.ModesTextList.RemoveAt(2);
        data.ModesTextList.Insert(2, new System.Tuple<string, bool>("Paint Value", false));
        EnterGeographicState();
    }
}