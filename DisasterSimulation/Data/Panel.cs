/****************************************************************************************/
/*
/* FILE NAME: Panel.cs
/*
/* DESCRIPTION: Displays a rectangle of a certain size in a specific position, to be used as an underlay for text, buttons, etc.
/*
/* REFERENCE: 
/*
/*      DATE                 BY                 DESCRIPTION
/* ========               =======               =============
/* 4/23/2016            Khaleel Harris          Created the class
/* 4/25/2016            Khaleel Harris          Removed references to static active panel
/*
/*
/*
/****************************************************************************************/

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class Panel
{
    public enum PanelModes { InspectMode, PaintMode}
    public PanelModes PanelMode;
    public bool IsActive;
    public RectangleShape PanelShape;
    public static List<Panel> PanelList = new List<Panel>();
    private static readonly Color FillColor_Inactive = new Color(120, 120, 120);
    private static readonly Color FillColor_Active = new Color(215, 215, 215);
    
    public Panel(Vector2f size, Vector2f position, PanelModes modeRef)
    {
        PanelShape = new RectangleShape(size);
        PanelShape.OutlineColor = new Color(0, 0, 0);
        PanelShape.FillColor = FillColor_Inactive;
        PanelShape.OutlineThickness = 2;
        PanelShape.Origin = new Vector2f(PanelShape.GetLocalBounds().Width / 2f, PanelShape.GetLocalBounds().Top);
        PanelShape.Position = position;

        PanelMode = modeRef;
        IsActive = false;
        PanelList.Add(this);
    }

/**
* @param value: Whether the panel should be enabled or disabled
* @return None
* @details Sets a panel active or inactivate depending on the argument <value>, which in turn changes the appearance of the panel
*/
    public void SetActive(bool value)
    {
        IsActive = value;
        PanelShape.OutlineThickness = IsActive ? 4 : 2;
        PanelShape.FillColor = IsActive ? FillColor_Active : FillColor_Inactive;
    }

/**
* @param modeToGet: The mode of the panel that is desired
* @return Panel in PanelList with mode that is equal to <modeToGet>. If none are found, returns null.
* @details Allows the user to get a Panel by its PanelMode value
*/
    public static Panel GetPanelByMode(PanelModes modeToGet)
    {
        foreach (Panel current in PanelList)
        {
            if (current.PanelMode == modeToGet)
            {
                return current;
            }
        }
        return null;
    }
}
