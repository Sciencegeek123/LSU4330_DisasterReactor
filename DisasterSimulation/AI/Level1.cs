using SFML.Graphics;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




class Level1
{
    //Reference Data
    Data data;
    Vector2u offset;
    Vector2u l1Size;

    Vector2u l1Origin; //The middle of this l1 commander.

    public float frameMagnitude = 1;

    byte BClamp(float f)
    {
        if (f > 255)
            return 255;
        else if (f < 0)
            return 0;
        else
            return (byte)f;
    }

    public void Initialize(Data _data, Vector2u _offset, Vector2u _size)
    {
        data = _data;
        offset = _offset;
        l1Size = _size;

        l1Origin = offset + l1Size / 2;
    }

    public float Update()
    {
        frameMagnitude = 0;

        for (uint i = offset.X; i < offset.X + l1Size.X; i++)
        {
            for (uint j = offset.Y; j < offset.Y + l1Size.Y; j++)
            {
                frameMagnitude += calculatePixelValue(i, j);
            }
        }

        frameMagnitude /= (l1Size.X * l1Size.Y);

        return frameMagnitude;
    }

    // check if position is within current commander bounds (look at offset)
    public Boolean isInMyRegion(Vector2u position)
    {
        if (position.X > offset.X + l1Size.X || position.X < offset.X)
            return false;            
        if (position.Y > offset.Y + l1Size.Y || position.Y < offset.Y)
            return false;
        return true;
    }

    public float calculatePixelValue(uint xpos, uint ypos)
    {
    
        //TODO Perhaps with some coefficients or something.

        Color EC = data.Environment.GetPixel(xpos, ypos);

        uint damage = EC.R;
        uint Difficulty = EC.B;
        uint Value = EC.G;

        Color TC = data.getPixel(xpos, ypos);

        uint aid = TC.G;
        uint repair = TC.B;
        if(Difficulty-repair == 0)
        {
            return (data.rand.Next() % 256)*((damage - aid) * Value);
        }
        

        return (data.rand.Next() % 256) * ((damage - aid) * Value) / (Difficulty - repair);
    }

    // input: vector2u position
    // output: vector2f with the calculations for each 

    public Vector2f calculateMagnitude(Vector2u position)
    {
        Vector2f magnitude = new Vector2f(0, 0);

        Vector2f direction = new Vector2f(0, 0);

        direction.X = (int)position.X - (int)l1Origin.X;
        direction.Y = (int)position.Y - (int)l1Origin.Y;

        float vecMag = Utilities.CalculateVector2fMagnitude(direction);

        if (Math.Abs(vecMag) < 0.001f)
        {
            direction = new Vector2f(0, 0); // normalize;
        } else
        {
            direction /= vecMag * vecMag; // normalize and distance penalty;
        }



        if (isInMyRegion(position))
        {
            for (uint i = offset.X; i < offset.X+l1Size.X; i++)
            {
                for(uint j= offset.Y; j < offset.Y + l1Size.Y; j++)
                {
                    magnitude += direction * calculatePixelValue(i, j);
                }
            }
            
        }
        else
        {
            return direction * frameMagnitude;
        }


        return magnitude / (l1Size.X * l1Size.Y);
    }

}

