using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

class Button
{
    public Sprite ButtonSprite;
    public enum ButtonFunctions { LoadMap, RunSim };
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

    public static Button GetButtonClicked()
    {
        foreach (Button current in ButtonList)
        {
            if (Mouse.GetPosition(RenderWindow).X <= current.ButtonSprite.Position.X + current.ButtonSprite.Texture.Size.X / 2f
            && Mouse.GetPosition(RenderWindow).X >= current.ButtonSprite.Position.X - current.ButtonSprite.Texture.Size.X / 2f
            && Mouse.GetPosition(RenderWindow).Y >= current.ButtonSprite.Position.Y - current.ButtonSprite.Texture.Size.Y / 2f
            && Mouse.GetPosition(RenderWindow).Y <= current.ButtonSprite.Position.Y + current.ButtonSprite.Texture.Size.Y / 2f)
            {
                System.Console.WriteLine(current.Function + " button clicked");
                return current;
            }
        }
        return null;
    }
}
