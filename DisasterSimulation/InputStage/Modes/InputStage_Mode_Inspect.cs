using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class InputStage : Stage
{

    void EnterInspectState()
    {
        //data.ModesTextList.RemoveAt(7);
        //data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", true));

        CurrentInputState = InputStates.Inspect;

        //data.ControlsTextList.Add(Keyboard.Key.R, new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        //data.Input.TrackKey(Keyboard.Key.R);

        //data.ControlsTextList.Add(Keyboard.Key.T, new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        //data.Input.TrackKey(Keyboard.Key.T);

        //data.ControlsTextList.Add(Keyboard.Key.Space, new System.Tuple<string, bool>("Space - Sample Cursor Position", false));
        //data.Input.TrackKey(Keyboard.Key.Space);

        //data.ControlsTextList.Add(Keyboard.Key.S, new System.Tuple<string, bool>("S - Spawn Point at Position", false));
        //data.Input.TrackKey(Keyboard.Key.S);

        //data.ControlsTextList.Add(Keyboard.Key.C, new System.Tuple<string, bool>("C - Clear Spawn Points", false));
        //data.Input.TrackKey(Keyboard.Key.C);

        CursorColor = Color.White;

        //data.InfoTextList.Add(new Tuple<string, bool>("(R, G, B) - Waiting for sample", false));

        //data.InfoTextList.Add(new Tuple<string, bool>("R - Waiting for sample", true));
        //data.InfoTextList.Add(new Tuple<string, bool>("G - Waiting for sample", true));
        //data.InfoTextList.Add(new Tuple<string, bool>("B - Waiting for sample", true));

    }

    void ProcessInspectState()
    {
        
        if (data.Input.CheckKeyHeld(Keyboard.Key.R))
        {
            if (CursorRadius < 256)
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
            data.ControlsTextList.RemoveAt(data.ControlsTextList.IndexOfKey(Keyboard.Key.Space));
            data.ControlsTextList.Add(Keyboard.Key.Space, new System.Tuple<string, bool>("Space - Sample Position\n\n(" + sample.R + ", " + sample.G + ", " + sample.B + ")", false));
            //data.InfoTextList.RemoveRange(data.InfoTextList.Count - 3, 3);
            //data.InfoTextList.RemoveRange(data.InfoTextList.Count - 1, 1);

            //data.InfoTextList.Add(new Tuple<string, bool>("(" + sample.R + ", " + sample.G + ", " + sample.B + ")", true));
            //data.InfoTextList.Add(new Tuple<string, bool>("R - " + sample.R, true));
            //data.InfoTextList.Add(new Tuple<string, bool>("G - " + sample.G, true));
            //data.InfoTextList.Add(new Tuple<string, bool>("B - " + sample.B, true));

        }

        //Check Spawn
        if(data.Input.CheckKeyPressed(Keyboard.Key.S))
        {
            //Vector2f point = ((Vector2f)Mouse.GetPosition() - (Vector2f)data.Graphics.ProgramWindow.Position) * 4.0f;
            Vector2f point = new Vector2f((Mouse.GetPosition(data.Graphics.ProgramWindow).X - data.Settings.InformationResolution.X), Mouse.GetPosition(data.Graphics.ProgramWindow).Y ); // new position for swapped windows

            data.SpawnPositions.Add(point);
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.C))
        {
            data.SpawnPositions.Clear();
        }

        //Check Transition
        //if (data.Input.CheckKeyPressed(Keyboard.Key.M))
        //    LeaveInspectState();


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
        data.Input.UntrackKey(Keyboard.Key.S);
        data.ControlsTextList.Remove(Keyboard.Key.S);
        data.Input.UntrackKey(Keyboard.Key.C);
        data.ControlsTextList.Remove(Keyboard.Key.C);
        CursorColor = Color.Black;

        //data.InfoTextList.RemoveRange(data.InfoTextList.Count - 3, 3);
        //data.InfoTextList.RemoveRange(data.InfoTextList.Count - 1, 1);

        //Transition
        //data.ModesTextList.RemoveAt(7);
        //data.ModesTextList.Insert(7, new System.Tuple<string, bool>("Inspect", false));
        EnterFinalizeState();
    }
    
}
