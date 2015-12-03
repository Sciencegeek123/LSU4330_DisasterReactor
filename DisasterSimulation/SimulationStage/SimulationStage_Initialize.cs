using SFML.Graphics;
using SFML.System;
using SFML.Window;

partial class SimulationStage : Stage
{
    int agentCount = 100;

    public override void Initialize(Data d)
    {

        data = d;
        data.InfoTextList.Add(new System.Tuple<string, bool>("Simulation Stage", true));
        data.Trails = new Image(4096, 4096, Color.Black);

        TEnv = new Texture(data.Environment);
        TTra = new Texture(data.Trails);

        SEnv = new Sprite(TEnv);
        STra = new Sprite(TTra);

        int offset = 0;
        if (data.SpawnPositions.Count != 0) {
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.Position = data.SpawnPositions[offset];
                a.data = new Color(32, 32, 0);
                offset = ++offset % data.SpawnPositions.Count;
                data.Agents.Add(a);
                
            }
        } else
        {
            for (int i = 0; i < agentCount; i++)
            {
                Agent a = new Agent();
                a.Position = new Vector2f(2048, 2048);
                a.data = new Color(32, 32, 0);
                offset = ++offset % data.SpawnPositions.Count;
                data.Agents.Add(a);

            }
        }
    }
}
