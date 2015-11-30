using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    public override void Initialize(Data d)
    {
        //Variable Initialization
        data = d;

        EnvironmentProduction = new RenderTexture(data.Settings.SimulationResolution.X, data.Settings.SimulationResolution.Y);
        EnvironmentProduction.Clear(Color.Black);

        CursorProduction = new RenderTexture(data.Settings.SimulationResolution.X, data.Settings.SimulationResolution.Y);
        CursorProduction.Clear(Color.Red);

        InfastructureList = new System.Collections.Generic.List<InfastructureHolder>();
        

        //Initial State
        CurrentInputState = InputStates.Difficulty;

        //Modes
        data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Difficulty", true));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Damage", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Value", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("-----", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Geographic Input", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Infastructure Input", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("-----", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Inspect", false));
        data.ModesTextList.Add(new System.Tuple<string, bool>("Finalize", false));

        //Information
        data.InfoTextList.Add(new System.Tuple<string, bool>("Input Stage", true));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Red   - Damage", false));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Green - Value", false));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Blue  - Difficulty", false));

        EnterDifficultyState();

        //Controls
        data.ControlsTextList.Add(Keyboard.Key.M, new System.Tuple<string, bool>("M - Switch Modes", false));
        data.Input.TrackKey(Keyboard.Key.M);
    }
}