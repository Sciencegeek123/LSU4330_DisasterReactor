using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System;

partial class GraphicsHolder
{
    Font RegularFont, BoldFont;
    Text HeaderText, ModesHeaderText, InfoHeaderText, ControlsHeaderText, TextTemplate;

    public void RenderInfo()
    {
        HeaderText.DisplayedString = "Disaster Simulation - CS3380 Project \nWilliam Jones & Sam Shrestha\nFrame " + data.Time.frame + "\n\nPress ESC to exit";

        ProgramInfoTexture.Draw(HeaderText);

        float lineSize = RegularFont.GetLineSpacing(data.Settings.InformationFontSize) * 1.1f;
        float offset = lineSize * 7f;

        ModesHeaderText.Position = new Vector2f(5, offset);
        ProgramInfoTexture.Draw(ModesHeaderText);
        offset += lineSize * 1.5f;

        //I should make this a function, but I'm not. :P

        foreach (Tuple<string, bool> str in data.ModesTextList)
        {
            TextTemplate.DisplayedString = str.Item1;
            TextTemplate.Position = new Vector2f(15, offset);

            if (str.Item2)
                TextTemplate.Font = BoldFont;
            else
                TextTemplate.Font = RegularFont;

            ProgramInfoTexture.Draw(TextTemplate);

            offset += lineSize;
        }
        offset += lineSize;

        ControlsHeaderText.Position = new Vector2f(5, offset);
        ProgramInfoTexture.Draw(ControlsHeaderText);
        offset += lineSize * 1.5f;

        foreach (Tuple<string, bool> str in data.ControlsTextList)
        {
            TextTemplate.DisplayedString = str.Item1;
            TextTemplate.Position = new Vector2f(15, offset);

            if (str.Item2)
                TextTemplate.Font = BoldFont;
            else
                TextTemplate.Font = RegularFont;

            ProgramInfoTexture.Draw(TextTemplate);

            offset += lineSize;
        }
        offset += lineSize;

        InfoHeaderText.Position = new Vector2f(5, offset);
        ProgramInfoTexture.Draw(InfoHeaderText);
        offset += lineSize * 1.5f;

        foreach (Tuple<string, bool> str in data.InfoTextList)
        {
            TextTemplate.DisplayedString = str.Item1;
            TextTemplate.Position = new Vector2f(15, offset);

            if (str.Item2)
                TextTemplate.Font = BoldFont;
            else
                TextTemplate.Font = RegularFont;

            ProgramInfoTexture.Draw(TextTemplate);

            offset += lineSize;
        }
    }
}

