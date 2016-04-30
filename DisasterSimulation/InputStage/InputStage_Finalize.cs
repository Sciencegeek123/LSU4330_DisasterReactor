using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    /// <summary>
    /// This function prepares the program for the simulation stage.
    /// It removes all of the GUI elements.
    /// </summary>
    /// <param name="d"></param>
    public override void Finalize(Data d)
    {
        data.ModesTextList.Clear();
        data.ControlsTextList.Clear();
        data.InfoTextList.Clear();
        Button.ButtonList.Clear();
        RadioButton.RadioButtonList.Clear();
        GraphicsHolder.SimStageLoaded = true;
        Panel.PanelList.RemoveAt(0); // remove inspect mode panel
    }
}