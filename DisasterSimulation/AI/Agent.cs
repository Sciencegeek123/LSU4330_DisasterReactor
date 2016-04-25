using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

class Agent
{

    /*
    Info: Energy, Aid, Last Gained Value
    Environment: R = Damage G = Value B = Difficulty
    Trails: R = Explore G = Aid B = Repair
    
    The Agent has the following options:
        1) Repair the current position
        2) Aid the current position
        3) Move to a neighboring position

    */
    public enum AgentActions
    {
        PERFORM_MOVE,
        PERFORM_AID,
        PERFORM_REPAIR,
    }

    public List<AgentActions> TurnActions;

    public Vector2u Position;
    public Vector2u NewPosition;

    public Color info = new Color(0, 0, 0);

    private Data data;
    private Overlord overlord;

    byte BClamp(float f)
    {
        if (f > 255)
            return 255;
        else if (f < 0)
            return 0;
        else
            return (byte)f;
    }

    private Color TC, EC;

    public float CalculateOffset(Vector2u offset)
    {
        Vector2u np = Position + offset;
        if (np.X > 4095 || np.X < 0 || np.Y > 4095 || np.Y < 0)
            return -1000;

        EC = data.Environment.GetPixel((uint)np.X, (uint)np.Y);
        TC = data.getPixel((int)np.X, (int)np.Y);

        return (data.rand.Next() % 512) * (((EC.R - TC.B) + (EC.G - TC.G) + (512 - TC.R - EC.B)) / 1024f);
    }

    public float CalculateRepair()
    {
        float repair = (float) (data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
        uint x = Position.X;
        uint y = Position.Y;
       // repair = repair + level0.Level0Value[level0.getArrayIndex(x,y).X, level0.getArrayIndex(x, y).Y].Y + level1.Level1Value[level1.getArrayIndex(x, y).X, level1.getArrayIndex(x, y).Y].Y;

        return repair;
    }

    public float CalculateAid()
    {
        float aid = (data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
        uint x = Position.X;
        uint y = Position.Y;
                    
        //aid = aid + level0.Level0Value[level0.getArrayIndex(x, y).X, level0.getArrayIndex(x, y).Y].X + level1.Level1Value[level1.getArrayIndex(x, y).X, level1.getArrayIndex(x, y).Y].X;

        return aid;
    }

    public void PerformRepair()
    {
        TurnActions.Add(AgentActions.PERFORM_REPAIR);

        if (TC.B < 255) TC.B++;
    }

    public void PerformAid()
    {
        TurnActions.Add(AgentActions.PERFORM_AID);

        if (TC.G < 255) TC.G++;
    }

    public void PerformMove(Vector2i offset)
    {
        TurnActions.Add(AgentActions.PERFORM_MOVE);

        NewPosition.X = (uint)(Position.X + offset.X);
        NewPosition.Y = (uint)(Position.Y + offset.Y);

        // Check if current square is completely explored
        if (TC.R < 255) TC.R++;


    }

    public void init(Data d, Vector2f p, Overlord overlord)
    {
        Position = new Vector2u((uint)p.X,(uint)p.Y);
        NewPosition = new Vector2u((uint)p.X, (uint)p.Y);
        data = d;
        this.overlord = overlord;
        TurnActions = new List<AgentActions>();

    }

    public void Update()
    {
        
        TurnActions.Clear();

        Position = NewPosition;

        TC = data.getPixel((int)Position.X, (int)Position.Y);
        EC = data.Environment.GetPixel((uint)Position.X, (uint)Position.Y);

        info.R = BClamp(info.R + 64); //Energy
        info.G = BClamp(info.G + 32); //Aid

        //To calculate direction use:
        Vector2f valueMagnitude = overlord.CalculateValueVector(Position); //This will be the same every iteration.


        while(info.R > 8)
        {

            //This is at the current position;
            float RepairVal = CalculateRepair();
            float AidVal = CalculateAid();

            float BestMove = 1; //Calculate the magnitude of the overlord calculation;

            info.R -= 8;

            if (BestMove > AidVal && BestMove > RepairVal)
            {
                //Take the direction of overlord calculation and calculate which of the 8 surrounding tiles it wants to move to.
                //This is a delta, so the elements have a range of -1 to 1

                Vector2i DeltaOffset = new Vector2i(0, 0);

                PerformMove(DeltaOffset);

                break;
            }
            else if (AidVal > RepairVal)
            {
                PerformAid();
            } else
            {
                PerformRepair();
            }

            if (data.rand.Next() % 256 > info.R)
                break;
        }
    }
}