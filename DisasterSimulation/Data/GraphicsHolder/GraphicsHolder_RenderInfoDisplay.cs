using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System;

partial class GraphicsHolder
{
    Font RegularFont, BoldFont;
    Text HeaderText, ModesHeaderText, InfoHeaderText, ControlsHeaderText, TextTemplate;

    public static bool SimStageLoaded;

    /// <summary>
    /// This panel renders the information pane on the left of the screen.
    /// </summary>
    public void RenderInfo()
    {
        foreach (Panel current in Panel.PanelList) // Drawing Info Overlay Panels
        {
            ProgramInfoTexture.Draw(current.PanelShape);
        }

        if (ErrorText.CurrentErrorText.showText) // show error text
        {
            TextTemplate.DisplayedString = ErrorText.CurrentErrorText.stringToDisplay;
            TextTemplate.Color = new Color(255, 0, 0, ErrorText.CurrentErrorText.alphaValue);
            TextTemplate.Origin = new Vector2f(TextTemplate.GetLocalBounds().Width / 2f, TextTemplate.GetLocalBounds().Height / 2f); // setting origin to center of text
            TextTemplate.Position = new Vector2f((LoadMapButton.ButtonSprite.Position.X + RunSimButton.ButtonSprite.Position.X) / 2f, LoadMapButton.ButtonSprite.Position.Y - 75); // positioning text between load map & run sim buttons
            ProgramInfoTexture.Draw(TextTemplate);

            if (ErrorText.CurrentErrorText.displayDurationRemaining > 0)
            {
                ErrorText.CurrentErrorText.displayDurationRemaining -= data.Time.deltaTime;
                if(ErrorText.CurrentErrorText.displayDurationRemaining <= 0)
                {
                    ErrorText.CurrentErrorText.displayDurationRemaining = 0;
                    ErrorText.CurrentErrorText.showText = false;
                }
            }
            ErrorText.CurrentErrorText.alphaValue = (byte)(255 * (ErrorText.CurrentErrorText.displayDurationRemaining / ErrorText.CurrentErrorText.displayDuration));
        }

        float lineSize = RegularFont.GetLineSpacing(data.Settings.InformationFontSize) * 1.1f;
        float offset = lineSize * 2f;

        if(SimStageLoaded)
        {
            InfoHeaderText.DisplayedString = "Toggle Options";
            InfoHeaderText.Origin = new Vector2f(InfoHeaderText.GetLocalBounds().Width / 2f, InfoHeaderText.GetLocalBounds().Height / 2f);
            InfoHeaderText.Position = new Vector2f(InspectModePanel.PanelShape.Position.X, offset + 20);
            offset += lineSize * 1.5f + 45;
        }
        else
        {
            InfoHeaderText.Position = new Vector2f(PaintModePanel.PanelShape.Position.X, offset + 15);
            offset += lineSize * 1.5f + 25;
        }
        ProgramInfoTexture.Draw(InfoHeaderText);

        foreach (Tuple<string, bool> str in data.InfoTextList) // Mode subtext
        {
            TextTemplate.DisplayedString = str.Item1;
            if (!InputStage.MapIsLoaded)
            {
                InfoHeaderText.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
                TextTemplate.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
            }
            else
            {
                InfoHeaderText.Color = Color.Black;
                TextTemplate.Color = Color.Black;
            }
            TextTemplate.Origin = new Vector2f(0, 0); // setting origin to center of text
            TextTemplate.Position = new Vector2f(100, offset);

            if (str.Item2)
                TextTemplate.Font = BoldFont;
            else
                TextTemplate.Font = RegularFont;

            ProgramInfoTexture.Draw(TextTemplate);

            offset += lineSize + 17f;
        }

        offset += lineSize + 215;

        ModesHeaderText.Origin = new Vector2f(ModesHeaderText.GetLocalBounds().Width / 2f, ModesHeaderText.GetLocalBounds().Height / 2f);
        ModesHeaderText.Position = new Vector2f(PaintModePanel.PanelShape.Position.X, offset - 175);
        if (!SimStageLoaded)
        {
            ProgramInfoTexture.Draw(ModesHeaderText);
        }
        offset += lineSize * 1.5f;

        foreach (var str in data.ControlsTextList) // inspect mode subtext
        {
            TextTemplate.DisplayedString = str.Value.Item1;
            if (!InputStage.MapIsLoaded || !InspectModePanel.IsActive)
            {
                ModesHeaderText.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
                TextTemplate.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
            }
            else
            {
                ModesHeaderText.Color = Color.Black;
                TextTemplate.Color = Color.Black;
            }
            if (TextTemplate.DisplayedString == "Please load a map" && !InputStage.MapIsLoaded) // handling the load a map text
            {
                TextTemplate.Origin = new Vector2f(TextTemplate.GetLocalBounds().Width / 2f, TextTemplate.GetLocalBounds().Height / 2f); // setting origin to center of text
                TextTemplate.Position = new Vector2f((LoadMapButton.ButtonSprite.Position.X + RunSimButton.ButtonSprite.Position.X) / 2f, LoadMapButton.ButtonSprite.Position.Y - 125); // positioning text between load map & run sim buttons
                TextTemplate.Font = RegularFont;
                TextTemplate.Color = Color.Black;
                ProgramInfoTexture.Draw(TextTemplate);
            }
            else if(TextTemplate.DisplayedString == "Please load a map" && InputStage.MapIsLoaded)
            {
                TextTemplate.DisplayedString = "";
                TextTemplate.Origin = new Vector2f(TextTemplate.GetLocalBounds().Width / 2f, TextTemplate.GetLocalBounds().Height / 2f); // setting origin to center of text
                TextTemplate.Position = new Vector2f((LoadMapButton.ButtonSprite.Position.X + RunSimButton.ButtonSprite.Position.X) / 2f, LoadMapButton.ButtonSprite.Position.Y - 125); // positioning text between load map & run sim buttons
                TextTemplate.Font = RegularFont;
                TextTemplate.Color = Color.Black;
                ProgramInfoTexture.Draw(TextTemplate);
            }
            else
            {
                TextTemplate.Origin = new Vector2f(0, 0);
                if (!SimStageLoaded)
                {
                    TextTemplate.Position = new Vector2f(ModesHeaderText.Position.X - 195, offset - 165);
                }
                else
                {
                    TextTemplate.Position = new Vector2f(InfoHeaderText.Position.X - 145, offset - 297);
                }

                if (str.Value.Item2)
                    TextTemplate.Font = BoldFont;
                else
                    TextTemplate.Font = RegularFont;

                ProgramInfoTexture.Draw(TextTemplate);

                offset += lineSize + 15;
            }
        }
    }
}

