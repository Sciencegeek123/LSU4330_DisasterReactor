using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System;

partial class GraphicsHolder
{
    /// <summary>
    /// This is a callback function for SFML when the "Red X" at the top of the window is clicked.
    /// </summary>
    /// <param name="sender">What object is initiating the callback.</param>
    /// <param name="e">What is the event that occured?</param>
    public void onWindowClose(object sender, EventArgs e)
    {
        data.Input.EscapeKeyPressed = true;
    }
}

