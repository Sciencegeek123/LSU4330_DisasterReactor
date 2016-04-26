using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class RadioButton
{
    public CircleShape ButtonShape_Outer, ButtonShape_Inner;
    public enum ButtonFunctions { PaintDifficulty, PaintDamage, PaintValue}
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

    public static RadioButton GetRadioButtonClicked()
    {
        foreach (RadioButton current in RadioButtonList)
        {
            if (Mouse.GetPosition(RenderWindow).X <= current.ButtonShape_Outer.Position.X + current.ButtonShape_Outer.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).X >= current.ButtonShape_Outer.Position.X - current.ButtonShape_Outer.GetLocalBounds().Width / 2f
            && Mouse.GetPosition(RenderWindow).Y >= current.ButtonShape_Outer.Position.Y - current.ButtonShape_Outer.GetLocalBounds().Height / 2f
            && Mouse.GetPosition(RenderWindow).Y <= current.ButtonShape_Outer.Position.Y + current.ButtonShape_Outer.GetLocalBounds().Height / 2f)
            {
                //System.Console.WriteLine(current.ButtonFunction);
                return current;
            }
        }
        return null;
    }

    public static void ReturnColorsToNormal()
    {
        foreach (RadioButton current in RadioButtonList) // return radio buttons to original colors
        {
            current.ButtonShape_Outer.FillColor = OuterCircle_FillColor;
            current.ButtonShape_Inner.FillColor = ActivatedRadioButton == current ? InnerCircle_FillColor_Activated : current.ButtonShape_Outer.FillColor;
        }
    }

    public static void FadeColors()
    {
        foreach (RadioButton current in RadioButtonList) // fade radio button colors since paint mode panel will be deactivated
        {
            current.ButtonShape_Outer.FillColor = OuterCircle_FillColorFaded;
            current.ButtonShape_Inner.FillColor = ActivatedRadioButton == current ? InnerCircle_FillColorFaded_Activated : new Color(OuterCircle_FillColorFaded.R, OuterCircle_FillColorFaded.G, OuterCircle_FillColorFaded.B, 0);
        }
    }
}
