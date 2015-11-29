using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class InputStage : Stage
{
    private void DrawCursor()
    {
        CircleShape CursorImage = new CircleShape();
        CursorImage.Origin = new Vector2f(CursorRadius*1.1f, CursorRadius*1.1f);
        CursorImage.OutlineColor = CursorColor;
        CursorImage.OutlineThickness = 8f;
        CursorImage.Radius = CursorRadius;
        CursorImage.FillColor = Color.Black;
        CursorImage.Position = ((Vector2f)Mouse.GetPosition() - (Vector2f)data.Graphics.ProgramWindow.Position) * 4.0f;
        CursorProduction.Draw(CursorImage);
    }

    float lastPress = -1;

    private void InteractCursor()
    {
        DrawCursor();

        if(data.Input.CheckKeyHeld(Keyboard.Key.R))
        {
            if(CursorRadius < 4096f)
                CursorRadius += data.Settings.RadiusStep * data.Time.deltaTime;
        } 

        if(data.Input.CheckKeyHeld(Keyboard.Key.T))
        {
            if(CursorRadius > 1.0f)
                CursorRadius -= data.Settings.RadiusStep * data.Time.deltaTime;
        }

        if(Mouse.IsButtonPressed(Mouse.Button.Left) && (data.Time.runTime - lastPress) > data.Settings.CursorPaintDelay)
        {
            lastPress = data.Time.runTime;
            CursorAdditive = true;
            PaintCursor();

        } else if(Mouse.IsButtonPressed(Mouse.Button.Right) && (data.Time.runTime - lastPress) > data.Settings.CursorPaintDelay)
        {
            lastPress = data.Time.runTime;
            CursorAdditive = false;
            PaintCursor();
        }
    }

    RenderStates Additive = new RenderStates(BlendMode.Add);
    RenderStates Subtractive = new RenderStates(
        new BlendMode(BlendMode.Factor.One, BlendMode.Factor.One, BlendMode.Equation.Subtract,
            BlendMode.Factor.One, BlendMode.Factor.One, BlendMode.Equation.Add));

    private void PaintCursor()
    {
        CircleShape CursorImage = new CircleShape();
        CursorImage.Origin = new Vector2f(CursorRadius * 1.1f, CursorRadius * 1.1f);
        CursorImage.OutlineColor = Color.Black;
        CursorImage.OutlineThickness = 8f;
        CursorImage.Radius = CursorRadius;
        CursorImage.FillColor = new Color(
            (byte)(CursorColor.R * data.Settings.CursorPaintStep), 
            (byte)(CursorColor.G * data.Settings.CursorPaintStep), 
            (byte)(CursorColor.B * data.Settings.CursorPaintStep));
        CursorImage.Position = ((Vector2f)Mouse.GetPosition() - (Vector2f)data.Graphics.ProgramWindow.Position) * 4.0f;
        if(CursorAdditive)
        {
            EnvironmentProduction.Draw(CursorImage, Additive);
        } else
        {
           EnvironmentProduction.Draw(CursorImage, Subtractive);
        }
    }
}