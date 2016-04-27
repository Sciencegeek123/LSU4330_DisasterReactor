﻿using SFML.System;
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
    //public static Panel ActivePanel;
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

    public void SetActive(bool value)
    {
        IsActive = value;
        PanelShape.OutlineThickness = IsActive ? 4 : 2;
        PanelShape.FillColor = IsActive ? FillColor_Active : FillColor_Inactive;
        //ActivePanel = this;
    }

    public static Panel GetInactivePanel()
    {
        foreach (Panel current in PanelList)
        {
            if (!current.IsActive)
            {
                return current;
            }
        }
        return null;
    }
}
