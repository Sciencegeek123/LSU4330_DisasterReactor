﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

partial class InputStage : Stage
{
    public override void Update()
    {
        // Hide Mouse Pointer while over simulation area
        bool hideMouseCursor = Mouse.GetPosition(Button.RenderWindow).X >= data.Settings.InformationResolution.X && Mouse.GetPosition(Button.RenderWindow).Y >= 0;
        Button.RenderWindow.SetMouseCursorVisible(!hideMouseCursor);

        CursorProduction.Clear(Color.Black);

        if (Mouse.IsButtonPressed(Mouse.Button.Left)) // handle regular button clicks
        {
            Button clickedButton = Button.GetButtonClicked();
            if(clickedButton!= null)
            {
                switch (clickedButton.Function)
                {
                    case Button.ButtonFunctions.LoadMap:
                        {
                            string FileName;
                            if (GetFileFromBrowser(out FileName))
                            {
                                InfastructureHolder InfLoadHolder = new InfastructureHolder();
                                InfLoadHolder.parseFile(FileName);
                                if (InfLoadHolder.isValid())
                                {
                                    Sprite env = new Sprite(InfLoadHolder.exportTextureResults());

                                    env.Origin = new Vector2f(2048, 2048);
                                    env.Position = new Vector2f(2048, 2048);
                                    env.Rotation = -90;

                                    EnvironmentProduction.Draw(env);
                                }
                            }
                            break;
                        }

                    case Button.ButtonFunctions.RunSim:
                        {
                            if (data.SpawnPositions.Count == 0)
                            {
                                if (SpawnWarning)
                                {
                                    SpawnWarning = false;
                                    data.InfoTextList.Add(new Tuple<string, bool>("At least one spawn must be set.", true));
                                }
                            }
                            else
                            {
                                EnvironmentProduction.Display();
                                data.Environment = EnvironmentProduction.Texture.CopyToImage();
                                PerformStageTransition = true;
                                return;
                            }
                            break;
                        }
                }
            }
        }

        if(Mouse.IsButtonPressed(Mouse.Button.Left) && Panel.ActivePanel.PanelMode == Panel.PanelModes.PaintMode) // handle radio button clicks
        {
            RadioButton ClickedButton = RadioButton.GetRadioButtonClicked();
            if(ClickedButton != null)
            {
                ClickedButton.SelectRadioButton();
                switch(ClickedButton.ButtonFunction)
                {
                    case RadioButton.ButtonFunctions.PaintDamage:
                        {
                            EnterDamageState();
                            break;
                        }

                    case RadioButton.ButtonFunctions.PaintDifficulty:
                        {
                            EnterDifficultyState();
                            break;
                        }

                    case RadioButton.ButtonFunctions.PaintValue:
                        {
                            EnterValueState();
                            break;
                        }
                }
            }
        }

        if (data.Input.CheckKeyPressed(Keyboard.Key.M)) // swap active panels
        {
            Panel PanelToActivate = Panel.GetInactivePanel();
            Panel.ActivePanel.SetActive(false);
            PanelToActivate.SetActive(true);
            //System.Console.WriteLine(PanelToActivate.PanelMode);
            if(PanelToActivate.PanelMode == Panel.PanelModes.InspectMode) // allow inspect mode input
            {
                data.Input.TrackKey(Keyboard.Key.S);
                data.Input.TrackKey(Keyboard.Key.C);
                data.Input.TrackKey(Keyboard.Key.R);
                data.Input.TrackKey(Keyboard.Key.T);
                data.Input.TrackKey(Keyboard.Key.Space);
                RadioButton.FadeColors(); // fade buttons since paint mode will be deactivated
                EnterInspectState();
            }
            else //disable key tracking for inspect mode
            {
                data.Input.UntrackKey(Keyboard.Key.S);
                data.Input.UntrackKey(Keyboard.Key.C);
                data.Input.UntrackKey(Keyboard.Key.R);
                data.Input.UntrackKey(Keyboard.Key.T);
                data.Input.UntrackKey(Keyboard.Key.Space);

                RadioButton.ReturnColorsToNormal(); // set radio buttons to normal colors since paint mode will be reactivated

                switch (RadioButton.ActivatedRadioButton.ButtonFunction)
                {
                    case RadioButton.ButtonFunctions.PaintDamage:
                        {
                            EnterDamageState();
                            break;
                        }

                    case RadioButton.ButtonFunctions.PaintDifficulty:
                        {
                            EnterDifficultyState();
                            break;
                        }

                    case RadioButton.ButtonFunctions.PaintValue:
                        {
                            EnterValueState();
                            break;
                        }
                }
            }
            Panel.ActivePanel = PanelToActivate;
        }

        if(Panel.ActivePanel != null && Panel.ActivePanel.PanelMode == Panel.PanelModes.InspectMode)
        {
            ProcessInspectState();
        }
        else // Paint Mode
        {
            InteractCursor();
            switch (CurrentInputState)
            {
                case InputStates.Damage:
                    {
                        ProcessDamageState();
                        break;
                    }

                case InputStates.Difficulty:
                    {
                        ProcessDifficultyState();
                        break;
                    }

                case InputStates.Value:
                    {
                        ProcessValueState();
                        break;
                    }
            }
        }

        EnvironmentProduction.Display();
        CursorProduction.Display();

        Sprite EnvironmentSprite = new Sprite(EnvironmentProduction.Texture);
        Sprite CursorSprite = new Sprite(CursorProduction.Texture);

        data.Graphics.ProgramDisplayTexture.Draw(EnvironmentSprite, new RenderStates(BlendMode.Add));
        data.Graphics.ProgramDisplayTexture.Draw(CursorSprite, new RenderStates(BlendMode.Add));
    }
}