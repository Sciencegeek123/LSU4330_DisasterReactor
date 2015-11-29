using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

class SettingHolder
{
    //Settings go here.
    public readonly Vector2u ScreenResolution = new Vector2u(1536, 1024);

    public readonly Vector2u SimulationResolution = new Vector2u(4096, 4096);
    public readonly Vector2f SimulationPosition = new Vector2f(0, 0);
    public readonly Vector2f SimulationScale = new Vector2f(0.25f, 0.25f);

    public readonly Vector2u InformationResolution = new Vector2u(512, 1024);
    public readonly Vector2f InformationPosition = new Vector2f(1024, 0);
    public readonly Vector2f InformationScale = new Vector2f(1, 1);

    public readonly uint InformationFontSize = 20;

    public readonly uint Sim_N_Workers = 12;

    public readonly float KeyPressDelay = 0.1f;

    public readonly float RenderTimeDelay = 0f;

    public readonly float RadiusStep = 128f;
    public readonly float CursorPaintStep = 0.03125f;
    public readonly float CursorPaintDelay = 0.033f;
}
