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
    private Color OuterCircle_FillColor = new Color(102, 178, 255);
    private Color InnerCircle_FillColor_Activated = Color.White;

    public RadioButton(Vector2f ButtonPosition, ButtonFunctions WhatToPaint)
    {
        CircleShape OuterCircle = new CircleShape(15f);
        OuterCircle.FillColor = OuterCircle_FillColor;
        OuterCircle.Origin = new Vector2f(OuterCircle.GetLocalBounds().Width / 2f, OuterCircle.GetLocalBounds().Height / 2f);
        OuterCircle.Position = ButtonPosition;
        ButtonShape_Outer = OuterCircle;

        ButtonShape_Inner = new CircleShape(ButtonShape_Outer.Radius * .50f);
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
            ActivatedRadioButton.IsActivated = true;
            ActivatedRadioButton.ButtonShape_Inner.FillColor = InnerCircle_FillColor_Activated;
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
                System.Console.WriteLine(current.ButtonFunction);
                return current;
            }
        }
        return null;
    }
}
