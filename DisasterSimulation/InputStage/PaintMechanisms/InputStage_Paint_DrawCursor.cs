using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

partial class InputStage : Stage
{
    private void DrawCursor()
    {
        CircleShape CursorImage = new CircleShape();
        CursorImage.Origin = new Vector2f(CursorRadius, CursorRadius);
        CursorImage.OutlineColor = CursorColor;
        CursorImage.OutlineThickness = 2f;
        CursorImage.Radius = CursorRadius;
        CursorImage.FillColor = Color.Transparent;
        CursorImage.Position = new Vector2f((Mouse.GetPosition(data.Graphics.ProgramWindow).X - data.Settings.InformationResolution.X), Mouse.GetPosition(data.Graphics.ProgramWindow).Y); // new position for swapped windows

        CursorProduction.Draw(CursorImage);
    }

    float lastPress = -1;

    private void InteractCursor()
    {
        DrawCursor();

        Console.Out.WriteLineAsync("IC - Cursor Status: " + Mouse.IsButtonPressed(Mouse.Button.Left));

        if(data.Input.CheckKeyHeld(Keyboard.Key.R))
        {
            if(CursorRadius < 256)
                CursorRadius += data.Settings.RadiusStep * data.Time.deltaTime;
        } 

        if(data.Input.CheckKeyHeld(Keyboard.Key.T))
        {
            if(CursorRadius > 1.0f)
                CursorRadius -= data.Settings.RadiusStep * data.Time.deltaTime;
        }

        if(Mouse.IsButtonPressed(Mouse.Button.Left) && (data.Time.runTime - lastPress) > data.Settings.CursorPaintDelay)
        {
            Console.Out.WriteLineAsync("IC - Painting Left");
            lastPress = data.Time.runTime;
            CursorAdditive = true;
            PaintCursor();

        } else if(Mouse.IsButtonPressed(Mouse.Button.Right) && (data.Time.runTime - lastPress) > data.Settings.CursorPaintDelay)
        {
            Console.Out.WriteLineAsync("IC - Painting Right");
            lastPress = data.Time.runTime;
            CursorAdditive = false;
            PaintCursor();
        }
    }

    private Color getAverageColor()
    {
        //Vector2f Position = ((Vector2f)Mouse.GetPosition() - (Vector2f)data.Graphics.ProgramWindow.Position) * 4.0f;
        Vector2f Position = new Vector2f((Mouse.GetPosition(data.Graphics.ProgramWindow).X - data.Settings.InformationResolution.X) * 4f, Mouse.GetPosition(data.Graphics.ProgramWindow).Y * 4f); // new position for swapped windows
        EnvironmentProduction.Display();
        Image icopy = EnvironmentProduction.Texture.CopyToImage();

        float R = 0, B = 0, G = 0;
        int n = 0;

        for(int x = -(int)Math.Ceiling(CursorRadius); x < (int)Math.Ceiling(CursorRadius); x++ )
        {
            if (x + Position.X < 0)
                continue;
            else if (x + Position.X > CursorProduction.Size.X)
                continue;

            for (int y = -(int)Math.Ceiling(CursorRadius); y < (int)Math.Ceiling(CursorRadius); y++)
            {
                if (y + Position.Y < 0)
                    continue;
                else if (y + Position.Y > CursorProduction.Size.Y)
                    continue;

                n++;
                Color pColor = icopy.GetPixel((uint)(x + Position.X), (uint)(y + Position.Y));
                R += pColor.R;
                G += pColor.G;
                B += pColor.B;
            }
        }

        return new Color((byte)(R / n), (byte)(G / n), (byte)(B / n));
    }

    RenderStates Additive = new RenderStates(BlendMode.Add);
    RenderStates Subtractive = new RenderStates(
        new BlendMode(BlendMode.Factor.One, BlendMode.Factor.One, BlendMode.Equation.Subtract,
            BlendMode.Factor.One, BlendMode.Factor.One, BlendMode.Equation.Add));

    private void PaintCursor()
    {
        Console.Out.WriteLineAsync("IC - Painting Generic");

        CircleShape CursorImage = new CircleShape();
        CursorImage.Origin = new Vector2f(CursorRadius, CursorRadius);
        CursorImage.OutlineColor = Color.Black;
        CursorImage.OutlineThickness = 0f;
        CursorImage.Radius = CursorRadius;
        CursorImage.FillColor = new Color(
            (byte)(CursorColor.R * data.Settings.CursorPaintStep), 
            (byte)(CursorColor.G * data.Settings.CursorPaintStep), 
            (byte)(CursorColor.B * data.Settings.CursorPaintStep));

        CursorImage.FillColor = Color.Red;
        CursorImage.Radius = 64;


        CursorImage.Position = new Vector2f((Mouse.GetPosition(data.Graphics.ProgramWindow).X - data.Settings.InformationResolution.X), Mouse.GetPosition(data.Graphics.ProgramWindow).Y)*4f; // new position for swapped windows

        if (CursorAdditive)
        {
            EnvironmentProduction.Draw(CursorImage, Additive);
        } else
        {
           EnvironmentProduction.Draw(CursorImage, Subtractive);
        }

        EnvironmentProduction.Display();
    }
}