using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterDamageState()
    {
        data.ModesTextList.RemoveAt(1);
        data.ModesTextList.Insert(1, new System.Tuple<string, bool>("Paint Damage", true));

        CurrentInputState = InputStates.Damage;
    }

    void ProcessDamageState()
    {
        //Update

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveDamageState();

    }

    void LeaveDamageState()
    {
        //Finalize

        //Transition
        data.ModesTextList.RemoveAt(1);
        data.ModesTextList.Insert(1, new System.Tuple<string, bool>("Paint Damage", false));
        EnterValueState();
    }
}