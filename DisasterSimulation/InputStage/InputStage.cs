using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

partial class InputStage : Stage
{
    //Put all variables here. Each function should be in a different file.
    RenderTexture EnvironmentProduction, CursorProduction;
    Data data;

    bool CursorAdditive = true;
    Color CursorColor = Color.Black;
    float CursorRadius = 256f;

    List<InfastructureHolder> InfastructureList;
    List<ElevationHolder> ElevationList;

    public static bool MapIsLoaded;

    enum InputStates
    {
        Difficulty,
        Damage,
        Value,
        Geographic,
        Infastructure,
        Inspect,
        Finalize
    }

    InputStates CurrentInputState;
}