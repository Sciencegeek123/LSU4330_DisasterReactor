using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    public override void Initialize(Data data)
    {
        data.ControlsTextList.Add(new System.Tuple<string, bool>("M - Switch Modes", false));
        data.ControlsTextList.Add(new System.Tuple<string, bool>("F - File Browser", false));

        data.Input.TrackKey(Keyboard.Key.F);
    }
}