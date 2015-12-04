using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


partial class SimulationStage : Stage
{
    Random rand = new Random();

    Texture TEnv, TTra;
    Sprite SEnv, STra;

    public override void Update()
    {

        foreach(Agent a in data.Agents)
        {
            int r = rand.Next() % 4;
            if(r == 0)
            {
                a.Position.X += (rand.Next() % 8) * 4;
            } else if(r == 1)
            {
                a.Position.X -= (rand.Next() % 8) * 4;
            } else if(r == 2)
            {
                a.Position.Y += (rand.Next() % 8) * 4;
            } else if(r == 3)
            {
                a.Position.Y -= (rand.Next() % 8) * 4;
            }

            if (a.Position.X < 0)
                a.Position.X = 0;
            else if (a.Position.X > 4095)
                a.Position.X = 4095;

            if (a.Position.Y < 0)
                a.Position.Y = 0;
            else if (a.Position.Y > 4095)
                a.Position.Y = 4095;

            Color p = data.Trails.GetPixel((uint)a.Position.X, (uint)a.Position.Y);

            p.R = (byte)(p.R + 32);
            p.G = (byte)(p.G + 32);
            p.B = (byte)(p.B + 32);

            data.Trails.SetPixel((uint)a.Position.X, (uint)a.Position.Y,p);
            p = data.Trails.GetPixel((uint)a.Position.X, (uint)a.Position.Y);

            Console.WriteLine("Color: " + p);

            a.data = Color.Magenta;
        }

        Console.WriteLine("Count: " + data.Agents.Count);
        data.Graphics.ProgramDisplayTexture.Clear(Color.Black);

        TTra.Update(data.Trails);
        TEnv.Update(data.Environment);

        SEnv.Texture = TEnv;
        STra.Texture = TTra;

        data.Graphics.ProgramDisplayTexture.Draw(SEnv, new RenderStates(BlendMode.Add));
        data.Graphics.ProgramDisplayTexture.Draw(STra, new RenderStates(BlendMode.Add));

        data.Graphics.ProgramDisplayTexture.Display();
    }
}
