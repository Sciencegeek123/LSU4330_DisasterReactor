﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    void EnterDamageState()
    {
        data.ModesTextList.RemoveAt(1);
        data.ModesTextList.Insert(1, new System.Tuple<string, bool>("Paint Damage", true));

        CursorColor = Color.Red;

        CurrentInputState = InputStates.Damage;

        data.Input.ClearTrackedKeys();

        data.ControlsTextList.Add(new System.Tuple<string, bool>("M - Switch Modes", false));
        data.Input.TrackKey(Keyboard.Key.M);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("R - Increase Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.R);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("T - Decrease Cursor Radius", false));
        data.Input.TrackKey(Keyboard.Key.T);

        data.ControlsTextList.Add(new System.Tuple<string, bool>("Left Mouse  - Paint Increase", false));
        data.ControlsTextList.Add(new System.Tuple<string, bool>("Right Mouse - Clear Paint", false));
    }

    void ProcessDamageState()
    {
        //Update
        DrawCursor();

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