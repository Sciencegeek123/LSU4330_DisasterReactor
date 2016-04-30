using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

/// <summary>
/// This file contains the information for preparing the simulation for the infastructure state.
/// It is completely obsolete.
/// </summary>
partial class InputStage : Stage
{

    void EnterInspectState()
    {
        CurrentInputState = InputStates.Inspect;
        CursorColor = Color.White;
    }

    void ProcessInspectState()
    {
        
        if (data.Input.CheckKeyHeld(Keyboard.Key.R))
        {
            if (CursorRadius < 1024f)
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
        }

        //Check Spawn
        if(data.Input.CheckKeyPressed(Keyboard.Key.S))
        {
            Vector2f point = new Vector2f((Mouse.GetPosition(data.Graphics.ProgramWindow).X - data.Settings.InformationResolution.X), Mouse.GetPosition(data.Graphics.ProgramWindow).Y ); // new position for swapped windows

            data.SpawnPositions.Add(point);
        }

        if(data.Input.CheckKeyPressed(Keyboard.Key.C))
        {
            data.SpawnPositions.Clear();
        }
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
        EnterFinalizeState();
    }
    
}
