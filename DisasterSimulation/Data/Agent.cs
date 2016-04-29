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


    public Vector2i Position;
    public Color info = new Color(0, 0, 0);

    private Data data;

    public byte BClamp(float f)
    {
        if (f > 255)
            return 255;
        else if (f < 0)
            return 0;
        else
            return (byte)f;
    }

    private Color TC, EC;

    public float CalculateOffset(Vector2i offset)
    {
        Vector2i np = Position + offset;
        if (np.X > 1023 || np.X < 0 || np.Y > 1023 || np.Y < 0)
            return -1000;

        EC = data.Environment.GetPixel((uint)np.X, (uint)np.Y);
        TC = data.getPixel((int)np.X, (int)np.Y);

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
        //EC R & TC B

        int dif = BClamp(EC.R - TC.B);

        if (dif > 32)
            dif = 32;

        if (dif > info.R)
            dif = info.R;
        if (dif > info.G)
            dif = info.G;

        info.R = BClamp(info.R - dif);
        info.B = BClamp(dif);

        TC.B = BClamp(TC.B + 255);

        data.setPixel(Position.X, Position.Y, TC);
        data.RepairHeatmap[Position.X, Position.Y]++;
    }

    public void PerformAid()
    {
        //EC G & TC G
        int dif = BClamp(EC.G - TC.G);

        if (dif > 32)
            dif = 32;

        if (dif > info.R)
            dif = info.R;
        if (dif > info.G)
            dif = info.G;

        info.R = BClamp(info.R - dif);
        info.B = BClamp(dif);

        TC.G = BClamp(TC.G + 255);

        data.setPixel(Position.X, Position.Y, TC);
        data.AidHeatmap[Position.X, Position.Y]++;
    }

    public void PerformMove(Vector2i offset)
    {
        Position += offset;

        EC = data.Environment.GetPixel((uint)Position.X, (uint)Position.Y);
        TC = data.getPixel(Position.X, Position.Y);
        
        TC.R = BClamp(TC.R + 8);
        info.R = BClamp(info.R - EC.B);
        data.setPixel(Position.X, Position.Y, TC);

        info.B = BClamp(TC.R + 8);
    }

    public void init(Data d, Vector2f p)
    {
        Position = new Vector2i((int)p.X,(int)p.Y);
        data = d;

    }

    public void paintPosition()
    {
        data.setPixel(Position.X, Position.Y, info);
    }
    public Data getData()
    {
        return data;
    }
    public void Update()
    {
        info.R = BClamp(info.R + 64); //Energy
        info.G = BClamp(info.G + 32); //Aid

        while(info.R > 8)
        {
            info.R = BClamp(info.R - 1);

            EC = data.Environment.GetPixel((uint)Position.X, (uint)Position.Y);
            TC = data.getPixel(Position.X, Position.Y);

            float RepairVal = CalculateRepair();
            float AidVal = CalculateAid();


            Vector2i BestOffset = new Vector2i(0, 0);
            float BestMove = -1000;


            int startLoc = (data.rand.Next() % (9 * 10000)) / 10000;
            for(int x = (startLoc + 1) % 9; x != startLoc; x = ++x % 9)
            {
                int i = x / 3;
                int j = (x - i * 3);

                i -= 1;
                j -= 1;
                
                if (i == 0 && j == 0)
                    continue;
                if ((Position.X + i) < 0 || (Position.X + i) > 1023 || (Position.Y + j) < 0 || (Position.Y + j) > 1023)
                    continue;

                float cal = CalculateOffset(new Vector2i(i, j));
                if (cal > BestMove)
                {
                    BestMove = cal;
                    BestOffset = new Vector2i(i, j);
                }
            }

            if (BestMove > AidVal && BestMove > RepairVal)
            {
                PerformMove(BestOffset);
            }
            else if (AidVal > RepairVal)
            {
                PerformAid();
            } else
            {
                PerformRepair();
            }

            data.PositionHeatmap[Position.X, Position.Y]++;

            if (data.rand.Next() % 256 > info.R)
                break;
        }
    }

    public void setEC(Color input)
    {
        EC = input;
    }
    public void setTC(Color input)
    {
        TC = input;
    }

    public Color getEC()
    {
        return EC;
    }
    public Color getTC()
    {
        return TC;
    }
}