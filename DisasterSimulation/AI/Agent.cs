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
        if (np.X > 1023 || np.X < 0 || np.Y > 1023 || np.Y < 0)
            return -1000;

        EC = data.Environment.GetPixel((uint)np.X, (uint)np.Y);
        TC = data.getPixel(np.X, np.Y);

        return (data.rand.Next() % 512) * (((EC.R - TC.B) + (EC.G - TC.G) + (512 - TC.R - EC.B)) / 1024f);
    }

    public float CalculateRepair()
    {
        return (data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
    }

    public float CalculateAid()
    {
        return (data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
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

        NewPosition.X = (uint)(Position.X - offset.X);

        if (offset.X < 0 && Position.X == 0)
        {
            NewPosition.X = 0;
        }
        else if (offset.X > 0 && Position.X == 1023)
        {
            NewPosition.X = 1023;
        }

        NewPosition.Y = (uint)(Position.Y - offset.Y);

        if (offset.Y < 0 && Position.Y == 0)
        {
            NewPosition.Y = 0;
        }
        else if (offset.Y > 0 && Position.Y == 1023)
        {
            NewPosition.Y = 1023;
        }

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

        TC = data.getPixel(Position.X, Position.Y);
        EC = data.Environment.GetPixel(Position.X, Position.Y);

        info.R = BClamp(info.R + 64); //Energy
        info.G = BClamp(info.G + 32); //Aid

        //To calculate direction use:
        Vector2f valueVector = overlord.CalculateValueVector(Position); //This will be the same every iteration.
        float valueMagnitude = Utilities.CalculateVector2fMagnitude(valueVector); //This will be the same every iteration.
        Vector2f valueNormal = valueVector / valueMagnitude; //This will be the same every iteration.

        Console.Out.WriteLine(valueVector.X + "," + valueVector.Y);

        while (info.R > 8)
        {

            //This is at the current position;
            float RepairVal = CalculateRepair();
            float AidVal = CalculateAid();

            float BestMove = valueMagnitude; //Calculate the magnitude of the overlord calculation;



            info.R -= 8;

            if (BestMove > AidVal && BestMove > RepairVal)
            {
                //Take the direction of overlord calculation and calculate which of the 8 surrounding tiles it wants to move to.
                //This is a delta, so the elements have a range of -1 to 1
                
                const float fhPI = 3.14159f / 2.0f;

                float xRand = ((float)data.rand.NextDouble())* 2.0f - 1.0f;
                float yRand = ((float)data.rand.NextDouble())* 2.0f - 1.0f;

                Vector2i DeltaOffset = new Vector2i( 
                    (int)Math.Round(valueNormal.X*(1.0f + xRand * 0.5f)),
                    (int)Math.Round(valueNormal.Y * (1.0f + yRand * 0.5f)));

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