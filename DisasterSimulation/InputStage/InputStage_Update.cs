using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Forms;

partial class InputStage : Stage
{ 
    public override void Update()
    {
        switch(CurrentInputState)
        {
            case InputStates.Difficulty:
                {
                    ProcessDifficultyState();
                    break;
                }
            case InputStates.Damage:
                {
                    ProcessDamageState();
                    break;
                }
            case InputStates.Value:
                {
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
    }
}