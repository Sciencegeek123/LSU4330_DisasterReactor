/****************************************************************************************/
/*
/* FILE NAME: Button.cs
/*
/* DESCRIPTION: Creates objects that register clicks within bounds in order to perform certain actions
/*
/* REFERENCE: 
/*
/*      DATE                 BY                 DESCRIPTION
/* ========               =======               =============
/* 3/26/2016            Khaleel Harris          Created the class
/* 4/25/2016            Khaleel Harris          Function to handle print created
/*
/*
/*
/****************************************************************************************/

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class Button
{
    public Sprite ButtonSprite;
    public enum ButtonFunctions { LoadMap, RunSim, PrintPDF, ExitSim };
    public ButtonFunctions Function;
    public static List<Button> ButtonList = new List<Button>();
    public static Window RenderWindow;
    public static Font ButtonFont;
    private Vector2f CenterPosition;

    public Button(string textureFile, Vector2f centerPosition, ButtonFunctions buttonFunction)
    {
        CenterPosition = centerPosition;

        ButtonSprite = new Sprite(new Texture(textureFile));
        ButtonSprite.Scale *= 1.25f;
        ButtonSprite.Texture.Smooth = true;
        ButtonSprite.Origin = new Vector2f(ButtonSprite.GetLocalBounds().Width / 2f, ButtonSprite.GetLocalBounds().Height / 2f);
        ButtonSprite.Position = centerPosition;

        Function = buttonFunction;
        ButtonList.Add(this);
    }

/**
* @param None
* @return Button object in ButtonList, if one was clicked. Null otherwise.
* @details Returns the button that was clicked based on mouse coordinates. If no button was clicked, returns null.
*/
    public static Button GetButtonClicked()
    {
        foreach (Button current in ButtonList)
        {
            if (Mouse.GetPosition(RenderWindow).X <= current.ButtonSprite.Position.X + current.ButtonSprite.Texture.Size.X / 2f
            && Mouse.GetPosition(RenderWindow).X >= current.ButtonSprite.Position.X - current.ButtonSprite.Texture.Size.X / 2f
            && Mouse.GetPosition(RenderWindow).Y >= current.ButtonSprite.Position.Y - current.ButtonSprite.Texture.Size.Y / 2f
            && Mouse.GetPosition(RenderWindow).Y <= current.ButtonSprite.Position.Y + current.ButtonSprite.Texture.Size.Y / 2f)
            {
                return current;
            }
        }
        return null;
    }
}
