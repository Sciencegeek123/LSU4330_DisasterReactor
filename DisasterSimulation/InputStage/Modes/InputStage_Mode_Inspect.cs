using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class InputStage : Stage
{

    void EnterInspectState()
    {
        data.ModesTextList.RemoveAt(7);
        data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", true));

        CurrentInputState = InputStates.Inspect;

        data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.R);

        data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.T);

        data.ControlsTextList.Add(Keyboard.Key.Space, new System.Tuple<string, bool>("Space - Sample Cursor Position", false));
        data.Input.TrackKey(Keyboard.Key.Space);

        CursorColor = Color.White;

        data.InfoTextList.Add(new Tuple<string, bool>("R - Waiting for sample", true));
        data.InfoTextList.Add(new Tuple<string, bool>("G - Waiting for sample", true));
        data.InfoTextList.Add(new Tuple<string, bool>("B - Waiting for sample", true));

    }

    void ProcessInspectState()
    {
        
        if (data.Input.CheckKeyHeld(Keyboard.Key.R))
        {
            if (CursorRadius < 4096f)
                CursorRadius += data.Settings.RadiusStep * data.Time.deltaTime;
        }

        if (data.Input.CheckKeyHeld(Keyboard.Key.T))
        {
            if (CursorRadius > 1.0f)
                CursorRadius -= data.Settings.RadiusStep * data.Time.deltaTime;
        }

        DrawCursor();

        //Update
        if (data.Input.CheckKeyPressed(Keyboard.Key.Space))
        {
            Color sample = getAverageColor();

            data.InfoTextList.RemoveRange(data.InfoTextList.Count - 3, 3);

            data.InfoTextList.Add(new Tuple<string, bool>("R - " + sample.R, true));
            data.InfoTextList.Add(new Tuple<string, bool>("G - " + sample.G, true));
            data.InfoTextList.Add(new Tuple<string, bool>("B - " + sample.B, true));

        }

        //Check Transition
        if (data.Input.CheckKeyPressed(Keyboard.Key.M))
            LeaveInspectState();


    }

    void LeaveInspectState()
    {
        //Cleanup
        data.Input.UntrackKey(Keyboard.Key.R);
        data.ControlsTextList.Remove(Keyboard.Key.R);
        data.Input.UntrackKey(Keyboard.Key.T);
        data.ControlsTextList.Remove(Keyboard.Key.T);
        data.Input.UntrackKey(Keyboard.Key.Space);
        data.ControlsTextList.Remove(Keyboard.Key.Space);
        CursorColor = Color.Black;
        
        data.InfoTextList.RemoveRange(data.InfoTextList.Count - 3, 3);

        //Transition
        data.ModesTextList.RemoveAt(7);
        data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", false));
        EnterFinalizeState();
    }
}
