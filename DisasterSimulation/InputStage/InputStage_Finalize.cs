using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    public override void Finalize(Data d)
    {
        data.ModesTextList.Clear();
        data.ControlsTextList.Clear();
        data.InfoTextList.Clear();

    }
}