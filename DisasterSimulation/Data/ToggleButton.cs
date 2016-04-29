/****************************************************************************************/
/*
/* FILE NAME: ToggleButton.cs
/*
/* DESCRIPTION: Creates objects that register clicks within bounds in order to enable certain functions
/*
/* REFERENCE: 
/*
/*      DATE                 BY                 DESCRIPTION
/* ========               =======               =============
/* 4/25/2016            Khaleel Harris          Created the class
/* 
/*
/*
/*
/****************************************************************************************/

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class ToggleButton
{
    public RectangleShape OuterShape, InnerShape;
    public enum ToggleOptions {ToggleAgents, ToggleSpawns, ToggleEnvironment, ToggleTrails }
    public ToggleOptions ToggleOption;
    public bool IsToggled;
    public static Window RenderWindow;
    public static List<ToggleButton> ToggleButtonList = new List<ToggleButton>();
    public static Color OuterShape_FillColor = new Color(160, 160, 160);
    public static Color InnerShape_FillColorActivated = Color.White;

    public ToggleButton(Vector2f ButtonPosition, ToggleOptions buttonOption)
    {
        OuterShape = new RectangleShape(new Vector2f(30f, 30f));
        OuterShape.FillColor = OuterShape_FillColor;
        OuterShape.Origin = new Vector2f(OuterShape.GetLocalBounds().Width / 2f, OuterShape.GetLocalBounds().Height / 2f);
        OuterShape.Position = ButtonPosition;

        InnerShape = new RectangleShape(OuterShape.Size/2f);
        InnerShape.FillColor = OuterShape.FillColor;
        InnerShape.Origin = new Vector2f(InnerShape.GetLocalBounds().Width / 2f, InnerShape.GetLocalBounds().Height / 2f);
        InnerShape.Position = OuterShape.Position;

        ToggleOption = buttonOption;

        ToggleButtonList.Add(this);
    }

/**
* @param shouldEnable: whether or not the toggle button should be enabled or disabled
* @return None
* @details Enables or disabled the toggle button, changing its appearance
*/
    public void ChangeToggleStatus(bool shouldEnable)
    {
        IsToggled = shouldEnable;
        InnerShape.FillColor = shouldEnable ? InnerShape_FillColorActivated : OuterShape_FillColor;
    }

/**
* @param None
* @return ToggleButton object in ToggleButtonList, if one was clicked. Null otherwise.
* @details Returns the toggle button that was clicked based on mouse coordinates. If no button was clicked, returns null.
*/
    public static ToggleButton GetToggleButtonClicked()
    {
        foreach (ToggleButton current in ToggleButtonList)
        {
            if (Mouse.GetPosition(RenderWindow).X <= current.OuterShape.Position.X + current.OuterShape.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).X >= current.OuterShape.Position.X - current.OuterShape.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).Y >= current.OuterShape.Position.Y - current.OuterShape.GetLocalBounds().Height / 2f
            && Mouse.GetPosition(RenderWindow).Y <= current.OuterShape.Position.Y + current.OuterShape.GetLocalBounds().Height / 2f)
            {
                return current;
            }
        }
        return null;
    }
}
