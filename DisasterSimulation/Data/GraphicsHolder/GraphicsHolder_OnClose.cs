using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System;

partial class GraphicsHolder
{
    public void onWindowClose(object sender, EventArgs e)
    {
        data.Input.EscapeKeyPressed = true;
    }
}

