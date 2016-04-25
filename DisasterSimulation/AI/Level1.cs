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

    /*
    Vector2f[,] Level1Value;
    */

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

    public float calculatePixelValue(uint i, uint j)
    {
        //Look at the environment and trails data for this pixel and come up with a function of how valuable this pixel is.
        
        //It's something like ((Damage - Aid) * Value / (Difficulty - Repair));

        //Perhaps with some coefficients or something.

        return 1;
    }

    // input: vector2u position
    // output: vector2f with the calculations for each 

    public Vector2f calculateMagnitude(Vector2u position)
    {
        Vector2f magnitude = new Vector2f(0, 0);

        Vector2f direction = new Vector2f(1, 1);

        direction.X = position.X - l1Origin.X;
        direction.Y = position.Y - l1Origin.Y;

        direction /= Utilities.CalculateVector2fMagnitude(direction); // normalize;


        if (isInMyRegion(position))
        {
            //TODO Look at every pixel in the region.
            for (uint i = offset.X; i < offset.X+l1Size.X; i++)
            {
                for(uint j= offset.Y; j < offset.Y + l1Size.Y; j++)
                {
                    // can change the frame magnitude 
                    // currently adds all vectors together (without looking at their individual values)
                    magnitude += direction * calculatePixelValue(i, j);
                }
            }
            
        }
        else
        {
            return direction * frameMagnitude;
        }


        return magnitude;
    }

}

