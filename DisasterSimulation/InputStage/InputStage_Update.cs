using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Forms;

partial class InputStage : Stage
{ 
    public override void Update()
    {
        CursorProduction.Clear(Color.Black);

        switch(CurrentInputState)
        {
            case InputStates.Difficulty:
                {
                    InteractCursor();
                    ProcessDifficultyState();
                    break;
                }
            case InputStates.Damage:
                {
                    InteractCursor();
                    ProcessDamageState();
                    break;
                }
            case InputStates.Value:
                {
                    InteractCursor();
                    ProcessValueState();
                    break;
                }
            case InputStates.Geographic:
                {
                    ProcessGeographicState();
                    break;
                }
            case InputStates.Infastructure:
                {
                    ProcessInfastructureState();
                    break;
                }
            case InputStates.Inspect:
                {
                    ProcessInspectState();
                    break;
                }
            case InputStates.Finalize:
                {
                    ProcessFinalizeState();
                    break;
                }
        }

        EnvironmentProduction.Display();
        CursorProduction.Display();

        Sprite EnvironmentSprite = new Sprite(EnvironmentProduction.Texture);
        Sprite CursorSprite = new Sprite(CursorProduction.Texture);

        data.Graphics.ProgramDisplayTexture.Draw(EnvironmentSprite,new RenderStates(BlendMode.Add));
        data.Graphics.ProgramDisplayTexture.Draw(CursorSprite, new RenderStates(BlendMode.Add));
    }
}