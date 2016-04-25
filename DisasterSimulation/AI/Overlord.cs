using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    This class will handle commmunication between the agents and commanders.

*/

class Overlord
{
    Vector2u commandersCount;
    Level0[,] commanders; //Each commander contains subcommanders.
    Data data;

    public void Initialize(Data d)
    {
        data = d;
        Vector2u l0Size = new Vector2u(512, 512);

        uint x, y;
        x = (d.Environment.Size.X + l0Size.X - 1) / l0Size.X; //Natural ceil function for unsigned division.
        y = (d.Environment.Size.X + l0Size.Y - 1) / l0Size.Y; //Natural ceil function for unsigned division. 

        commandersCount = new Vector2u(x, y);

        commanders = new Level0[x, y];

        for (uint i = 0; i < x; i++)
        {
            for(uint j = 0; j < y; j++)
            {
                commanders[i, j] = new Level0();
                commanders[i, j].Initialize(data, new Vector2u(i * l0Size.X, j * l0Size.Y), l0Size);
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < commandersCount.X; i++)
        {
            for (int j = 0; j < commandersCount.Y; j++)
            {
                commanders[i, j].Update();
            }
        }
    }

    public Vector2f CalculateValueVector(Vector2u position)
    {
        Vector2f value = new Vector2f(0, 0);
        
        for (int i = 0; i < commandersCount.X; i++)
        {
            for (int j = 0; j < commandersCount.Y; j++)
            {
                value += commanders[i, j].calculateMagnitudeVector(position);
            }
        }
        return value;
    }
}

