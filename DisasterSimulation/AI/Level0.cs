using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Collections;

class Level0
{
    //Reference Data
    Data data;
    Vector2u offset;
    Vector2u l0Size;

    Level1[,] subCommanders;
    Vector2u subCommandersCount;

    Vector2u l0Origin; //The middle of this L0 Commander;
    public Vector2f magnitude;
    public Vector2f totalMagnitude;
    float frameMagnitude = 1; //The arbitrary value of the entire l0 region. Calculated once per frame.

    /*
    Vector2f[,] Level0Values;
    */

    public void Initialize(Data _data, Vector2u _offset, Vector2u _size)
    {


        data = _data;
        offset = _offset; //Starting X and Y position. It is responsible for offset -> offset * size. (Though x and y need to be calculated seperately.)
        l0Size = _size;

        Vector2u l1Size = new Vector2u(32, 32);

        uint x, y;
        x = (l0Size.X + l1Size.X - 1) / l1Size.X; //Natural ceil function for unsigned division.
        y = (l0Size.X + l1Size.Y - 1) / l1Size.Y; //Natural ceil function for unsigned division. 

        l0Origin = offset + l0Size / 2;

        subCommandersCount = new Vector2u(x, y);

        subCommanders = new Level1[x, y];

        for(uint i = 0; i < x; i++)
        {
            for(uint j = 0; j < y; j++)
            {
                subCommanders[i, j] = new Level1();
                subCommanders[i, j].Initialize(data, new Vector2u(i * l1Size.X, j * l1Size.Y), l0Size);
            }
        }
    }

    //Calculate cached values for each iteration.
    public void Update()
    {
        //gets the value from the origin for use with other 
        settotalMagnitude();
    }
    // check if position is within current commander bounds (look at offset)
    public Boolean isInMyRegion(Vector2u position)
    {
        bool value = true;
        if (position.X > offset.X + l0Size.X / 2 || position.X < offset.X - l0Size.X / 2)
            value = false;
        if (position.Y > offset.Y + l0Size.Y / 2 || position.Y < offset.Y - l0Size.Y / 2)
            value = false;

        return value;
    }

    public Vector2f calculateMagnitude(Vector2u position)
    {
        
        magnitude = new Vector2f(0, 0);


        if(isInMyRegion(position))
        {
            for (int i = 0; i < subCommandersCount.X; i++)
            {
                for (int j = 0; j < subCommandersCount.X; j++)
                {
                    magnitude += subCommanders[i, j].calculateMagnitude(position);
                }

            }

        } else
        {
            Vector2f direction = new Vector2f(1, 1); //TODO You need to determine;
            

            return direction * frameMagnitude;
        }


        return magnitude;
    }

    public void settotalMagnitude()
    {
        totalMagnitude = calculateMagnitude(l0Origin);
    }

    public uint distancefromAgent(Vector2f agentpos)
    {
        uint distance;
        distance = (uint)Math.Sqrt((agentpos.X - l0Origin.X) * (agentpos.X - l0Origin.X) + (agentpos.Y - l0Origin.Y) * (agentpos.Y - l0Origin.Y));

        return distance;
    }
   

}

