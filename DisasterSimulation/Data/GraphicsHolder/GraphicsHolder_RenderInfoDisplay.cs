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

    public void RenderInfo()
    {
        foreach (Panel current in Panel.PanelList) // Drawing Info Overlay Panels
        {
            ProgramInfoTexture.Draw(current.PanelShape);
        }

        System.Console.WriteLine("dt: " + data.Time.deltaTime);
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

        //HeaderText.DisplayedString = "Disaster Simulation - CS3380 Project \nWilliam Jones & Sam Shrestha\nFrame " + data.Time.frame + "\n\nPress ESC to exit";
        //ProgramInfoTexture.Draw(HeaderText);

        float lineSize = RegularFont.GetLineSpacing(data.Settings.InformationFontSize) * 1.1f;
        //float offset = lineSize * 7f;
        float offset = lineSize * 2f;

        if(SimStageLoaded)
        {
            ModesHeaderText.DisplayedString = "Toggle Options";
            ModesHeaderText.Origin = new Vector2f(ModesHeaderText.GetLocalBounds().Width / 2f, ModesHeaderText.GetLocalBounds().Height / 2f);
            ModesHeaderText.Position = new Vector2f(InspectModePanel.PanelShape.Position.X, offset + 20);
            offset += lineSize * 1.5f + 45;
        }
        else
        {
            ModesHeaderText.Position = new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 80, offset);
            offset += lineSize * 1.5f + 25;
        }
        ProgramInfoTexture.Draw(ModesHeaderText);

        foreach (var str in data.ControlsTextList) // inspect mode subtext
        {
            TextTemplate.DisplayedString = str.Value.Item1;
            if (Panel.ActivePanel.PanelMode == Panel.PanelModes.PaintMode && !SimStageLoaded) // change font color to hint that panel is disabled
            {
                ModesHeaderText.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
                TextTemplate.Color = new Color(175, 175, 175, (byte)(255 * 0.45f));
            }
            else
            {
                ModesHeaderText.Color = Color.Black;
                TextTemplate.Color = Color.Black;
            }
            if (TextTemplate.DisplayedString == "Press M to switch modes") // moving switch modes text into specific pos
            {
                TextTemplate.Origin = new Vector2f(TextTemplate.GetLocalBounds().Width / 2f, TextTemplate.GetLocalBounds().Height / 2f); // setting origin to center of text
                TextTemplate.Position = new Vector2f((LoadMapButton.ButtonSprite.Position.X + RunSimButton.ButtonSprite.Position.X)/2f, LoadMapButton.ButtonSprite.Position.Y - 125); // positioning text between load map & run sim buttons
                TextTemplate.Font = RegularFont;
                TextTemplate.Color = Color.Black;
                ProgramInfoTexture.Draw(TextTemplate);
            }
            else
            {
                TextTemplate.Origin = new Vector2f(0, 0);
                if(!SimStageLoaded)
                {
                    TextTemplate.Position = new Vector2f(ModesHeaderText.Position.X - 60, offset);
                }
                else
                {
                    TextTemplate.Position = new Vector2f(InfoHeaderText.Position.X - 30, offset);
                }

                if (str.Value.Item2)
                    TextTemplate.Font = BoldFont;
                else
                    TextTemplate.Font = RegularFont;

                ProgramInfoTexture.Draw(TextTemplate);

                offset += lineSize + 15;
            }
        }

        offset += lineSize + 215;

        InfoHeaderText.Position = new Vector2f(PaintModePanel.PanelShape.GetGlobalBounds().Width / 2f - 60, offset - 100);
        if(!SimStageLoaded)
        {
            ProgramInfoTexture.Draw(InfoHeaderText);
        }
        offset += lineSize * 1.5f;

        foreach (Tuple<string, bool> str in data.InfoTextList) // Paint Mode subtext
        {
            TextTemplate.DisplayedString = str.Item1;
            if (Panel.ActivePanel.PanelMode == Panel.PanelModes.InspectMode && !SimStageLoaded) // change font color to hint that panel is disabled
            {

                InfoHeaderText.Color = new Color(175, 175, 175, (byte)(255*0.45f));
                TextTemplate.Color = new Color(175, 175, 175, (byte)(255*0.45f));
            }
            else
            {
                InfoHeaderText.Color = Color.Black;
                TextTemplate.Color = Color.Black;
            }
            //TextTemplate.Position = new Vector2f(15, offset);
            TextTemplate.Position = new Vector2f(InfoHeaderText.Position.X - 30, offset - 80); 

            if (str.Item2)
                TextTemplate.Font = BoldFont;
            else
                TextTemplate.Font = RegularFont;

            ProgramInfoTexture.Draw(TextTemplate);

            offset += lineSize + 17f;
        }
    }
}

