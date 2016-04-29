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
        //CurrentInputState = InputStates.Inspect;

        //INSPECT MODE ACTION TEXT
        //data.ControlsTextList.Add(Keyboard.Key.S, new System.Tuple<string, bool>("S - Spawn Point at Position", false));
        data.ControlsTextList.Add(Keyboard.Key.S, new System.Tuple<string, bool>("S - Create Spawn Point", false));
        data.ControlsTextList.Add(Keyboard.Key.C, new System.Tuple<string, bool>("C - Clear Spawn Points", false));
        //data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Radius", false));
        //data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Radius", false));
        //data.ControlsTextList.Add(Keyboard.Key.Space, new System.Tuple<string, bool>("Space - Sample Cursor Position", false));
        data.ControlsTextList.Add(Keyboard.Key.Space, new System.Tuple<string, bool>("Space - Sample Position\n\n(R, G, B)", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Difficulty", true));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Difficulty", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Damage", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Paint Value", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("-----", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Geographic Input", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Infastructure Input", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("-----", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Inspect", false));
        //data.ModesTextList.Add(new System.Tuple<string, bool>("Finalize", false));

        //Information
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Input Stage", true));
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Red   - Damage", false));
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Green - Value", false));
        //data.InfoTextList.Add(new System.Tuple<string, bool>("Blue  - Difficulty", false));

        //PAINT MODE ACTION TEXT
        data.InfoTextList.Add(new System.Tuple<string, bool>("Inspect Mode", false));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Paint Mode (Damage)", false));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Paint Mode (Difficulty)", false));
        data.InfoTextList.Add(new System.Tuple<string, bool>("Paint Mode (Value)", false));

        //Controls
        //data.ControlsTextList.Add(Keyboard.Key.M, new System.Tuple<string, bool>("Press M to switch modes", false));
        data.ControlsTextList.Add(Keyboard.Key.M, new System.Tuple<string, bool>("Please load a map", false));
        data.Input.TrackKey(Keyboard.Key.M);
        data.Input.TrackKey(Keyboard.Key.S);
        data.Input.TrackKey(Keyboard.Key.C);
        data.Input.TrackKey(Keyboard.Key.R);
        data.Input.TrackKey(Keyboard.Key.T);
        data.Input.TrackKey(Keyboard.Key.Space);

        RadioButton.FadeColors(); // fade colors since all panels are deactivated until map is loaded

        EnterInspectState();
    }
}