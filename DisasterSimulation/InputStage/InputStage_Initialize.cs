using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    public override void Initialize(Data d)
    {
        data = d;

        //Initial State
        CurrentInputState = InputStates.Geographic;

        //Modes
        data.ModesTextList.Add(new System.Tuple<string, bool>("Geographic Input", true));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Infastructure Input", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Damage", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Value", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Finalize", false));

        //Controls
        data.Input.ClearTrackedKeys();

        data.ControlsTextList.Add(new System.Tuple<string, bool>("M - Switch Modes", false));
        data.Input.TrackKey(Keyboard.Key.M);

    }
}