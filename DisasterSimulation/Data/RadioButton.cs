/****************************************************************************************/
/*
/* FILE NAME: RadioButton.cs
/*
/* DESCRIPTION: Creates objects that register clicks within bounds in order to perform certain actions while disabling the actions of other connected radio buttons
/*
/* REFERENCE: 
/*
/*      DATE                 BY                 DESCRIPTION
/* ========               =======               =============
/* 4/23/2016            Khaleel Harris          Created the class
/* 4/27/2016            Khaleel Harris          Created new ButtonFunction: Inspect
/*
/*
/*
/****************************************************************************************/

using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class RadioButton
{
    public CircleShape ButtonShape_Outer, ButtonShape_Inner;
    public enum ButtonFunctions { PaintDifficulty, PaintDamage, PaintValue, Inspect }
    public ButtonFunctions ButtonFunction;
    public static Window RenderWindow;
    public static List<RadioButton> RadioButtonList = new List<RadioButton>();
    public static RadioButton ActivatedRadioButton;
    private bool IsActivated;
    private readonly static Color OuterCircle_FillColor = new Color(160, 160, 160);
    private readonly static Color OuterCircle_FillColorFaded = new Color(160, 160, 160, (byte)(255 * 0.45f));
    private readonly static Color InnerCircle_FillColor_Activated = Color.White;
    private readonly static Color InnerCircle_FillColorFaded_Activated = new Color(255, 255, 255, (byte)(255 * 0.45f));

    public RadioButton(Vector2f ButtonPosition, ButtonFunctions WhatToPaint)
    {
        CircleShape OuterCircle = new CircleShape(15f);
        OuterCircle.FillColor = OuterCircle_FillColor;
        OuterCircle.Origin = new Vector2f(OuterCircle.GetLocalBounds().Width / 2f, OuterCircle.GetLocalBounds().Height / 2f);
        OuterCircle.Position = ButtonPosition;
        ButtonShape_Outer = OuterCircle;

        ButtonShape_Inner = new CircleShape(ButtonShape_Outer.Radius * 0.50f);
        ButtonShape_Inner.FillColor = ButtonShape_Outer.FillColor;
        ButtonShape_Inner.Origin = new Vector2f(ButtonShape_Inner.GetLocalBounds().Width / 2f, ButtonShape_Inner.GetLocalBounds().Height / 2f);
        ButtonShape_Inner.Position = ButtonShape_Outer.Position;

        ButtonFunction = WhatToPaint;

        RadioButtonList.Add(this);
    }

/**
* @param None
* @return None
* @details Sets this radio button as 'selected', changing its appearance
*/
    public void SelectRadioButton()
    {
        if(ActivatedRadioButton != this)
        {
            if(ActivatedRadioButton != null)
            {
                ActivatedRadioButton.IsActivated = false;
                ActivatedRadioButton.ButtonShape_Inner.FillColor = OuterCircle_FillColor;
            }

            ActivatedRadioButton = this;
            IsActivated = true;
            ButtonShape_Inner.FillColor = InnerCircle_FillColor_Activated;
        }
    }

/**
* @param None
* @return None
* @details Sets this radio button as 'deselected', changing its appearance
*/
    public void DeselectRadioButton()
    {
        ActivatedRadioButton = null;
        IsActivated = false;
        ButtonShape_Inner.FillColor = ButtonShape_Outer.FillColor;
    }

/**
* @param funcToGet: the ButtonFunction of the radio button desired
* @return RadioButton object in RadioButtonList whose ButtonFunction is equal to <funcToGet>. If none are found, returns null;
* @details Gets a RadioButton whose ButtonFunction matches the value of the <funcToGet> argument. If none are found it will return null instead.
*/
    public static RadioButton GetRadioButtonByFunction(ButtonFunctions funcToGet)
    {
        foreach(RadioButton current in RadioButtonList)
        {
            if(current.ButtonFunction == funcToGet)
            {
                return current;
            }
        }
        return null;
    }

/**
* @param None
* @return RadioButton object in RadioButtonList, if one was clicked. Null otherwise.
* @details Returns the radio button that was clicked based on mouse coordinates. If no button was clicked, returns null.
*/
    public static RadioButton GetRadioButtonClicked()
    {
        foreach (RadioButton current in RadioButtonList)
        {
            if (Mouse.GetPosition(RenderWindow).X <= current.ButtonShape_Outer.Position.X + current.ButtonShape_Outer.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).X >= current.ButtonShape_Outer.Position.X - current.ButtonShape_Outer.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).Y >= current.ButtonShape_Outer.Position.Y - current.ButtonShape_Outer.GetLocalBounds().Height / 2f
            && Mouse.GetPosition(RenderWindow).Y <= current.ButtonShape_Outer.Position.Y + current.ButtonShape_Outer.GetLocalBounds().Height / 2f)
            {
                return current;
            }
        }
        return null;
    }

/**
* @param None
* @return None
* @details Sets the colors of all radio buttons in the RadioButtonList to their "interactable" color
*/
    public static void ReturnColorsToNormal()
    {
        foreach (RadioButton current in RadioButtonList) // return radio buttons to original colors
        {
            current.ButtonShape_Outer.FillColor = OuterCircle_FillColor;
            current.ButtonShape_Inner.FillColor = ActivatedRadioButton == current ? InnerCircle_FillColor_Activated : current.ButtonShape_Outer.FillColor;
        }
    }

/**
* @param None
* @return None
* @details Sets the colors of all radio buttons in the RadioButtonList to their "non-interactable" color
*/
    public static void FadeColors()
    {
        foreach (RadioButton current in RadioButtonList) // fade radio button colors since paint mode panel will be deactivated
        {
            current.ButtonShape_Outer.FillColor = OuterCircle_FillColorFaded;
            current.ButtonShape_Inner.FillColor = ActivatedRadioButton == current ? InnerCircle_FillColorFaded_Activated : new Color(OuterCircle_FillColorFaded.R, OuterCircle_FillColorFaded.G, OuterCircle_FillColorFaded.B, 0);
        }
    }
}
