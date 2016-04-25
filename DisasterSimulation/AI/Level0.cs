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

    float frameMagnitude = 1; //The value of the entire l0 region. Calculated once per frame.

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
                subCommanders[i, j].Initialize(data, new Vector2u(i * l1Size.X + offset.X, j * l1Size.Y + offset.Y), l1Size);
            }
        }
    }

    //Calculate cached values for each frame.
    public void Update()
    {
        frameMagnitude = 0;
        var calculatePixelTime = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i < subCommandersCount.X; i++)
        {
            for (int j = 0; j < subCommandersCount.Y; j++)
            {
                frameMagnitude += subCommanders[i, j].Update();
            }
        }
        calculatePixelTime.Stop();
        Console.WriteLine("CalculatePixelTime: " + calculatePixelTime.ElapsedMilliseconds);
    }
    
    // check if position is within current commander bounds (look at offset)
    public Boolean isInMyRegion(Vector2u position)
    {
        if (position.X > offset.X + l0Size.X || position.X < offset.X)
            return false;
        if (position.Y > offset.Y + l0Size.Y || position.Y < offset.Y)
            return false;
        return true;
    }

    public Vector2f calculateMagnitudeVector(Vector2u position)
    {
        
        Vector2f magnitude = new Vector2f(0, 0);


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
            Vector2f direction = new Vector2f(1, 1);

            direction.X = position.X - l0Origin.X;
            direction.Y = position.Y - l0Origin.Y;

            direction /= Utilities.CalculateVector2fMagnitude(direction); // normalize;
            
            return direction * frameMagnitude;
        }


        return magnitude;
    }  
}

