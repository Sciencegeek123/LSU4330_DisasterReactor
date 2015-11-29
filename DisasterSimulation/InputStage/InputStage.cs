using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    //Put all variables here. Each function should be in a different file.
    RenderTexture InputGenerationWindow;
    Data data;

    enum InputStates
    {
        Geographic,
        Infastructure,
        Damage,
        Value,
        Finalize
    }

    InputStates CurrentInputState = InputStates.Geographic;
}