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

    float frameMagnitude = 1;

    /*
    Vector2f[,] Level1Value;
    */

    public void Initialize(Data _data, Vector2u _offset, Vector2u _size)
    {
        data = _data;
        offset = _offset;
        l1Size = _size;

        l1Origin = offset + l1Size / 2;


        /*
        double width = input.getWidth();
        double height = input.getHeight();
        level0 = input;

        Level1Value = new Vector2f[4, 4];
        xoffset = Math.Ceiling(width / 4);
        yoffset = Math.Ceiling(height / 4);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //double check... that they are in the right order 
                Level1Value[i, j] = calculateValue((int)(i * xoffset - 1), (int)(j * yoffset - 1), Math.Ceiling(width), Math.Ceiling(height));
            }
        }
        */

    }
    // check if position is within current commander bounds (look at offset)
    public Boolean isInMyRegion(Vector2u position)
    {
        bool value = true;
        if (position.X > offset.X + l1Size.X / 2 || position.X < offset.X - l1Size.X / 2)
            value = false;
        if (position.Y > offset.Y + l1Size.Y / 2 || position.Y < offset.Y - l1Size.Y / 2)
            value = false;

        return value;
    }


    // input: vector2u position
    // output: vector2f with the calculations for each 

    public Vector2f calculateMagnitude(Vector2u position)
    {
        Vector2f magnitude = new Vector2f(0, 0);
        
        if (isInMyRegion(position))
        {
            Vector2u start = l1Origin - offset - l1Size / 2;

            //TODO Look at every pixel in the region.
            for (uint i =start.X; i < start.X+l1Size.X; i++)
            {
                for(uint j=start.Y; j < start.Y + l1Size.Y; j++)
                {
                    // can change the frame magnitude 
                    // currently adds all vectors together (without looking at their individual values)
                    magnitude += new Vector2f(i, j)*frameMagnitude;
                }
            }
            
        }
        else
        {
            Vector2f direction = new Vector2f(1, 1); //You need to determine;
            return direction * frameMagnitude;
        }


        return magnitude;
        /*
        float totalaid = 0;
        float totalrepair = 0;
        float count = 0;
        Vector2f returnvalue = new Vector2f();
        Color TC, EC;

        for (int x = getArrayIndex(xcoord, ycoord).X * (int)xoffset; x < (getArrayIndex(xcoord, ycoord).X + 1) * (int)xoffset; x++)
        {
            for (int y = getArrayIndex(xcoord, ycoord).Y * (int)yoffset; y < (getArrayIndex(xcoord, ycoord).Y + 1) * (int)yoffset; y++)
            {
                EC = level0.data.Environment.GetPixel((uint)x, (uint)y);
                TC = level0.data.getPixel(x, y);
                totalaid += (level0.data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
                totalrepair += (level0.data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
                count++;
            }
        }


        returnvalue.X = totalaid;
        returnvalue.Y = totalrepair;
        return returnvalue / count;
        */
    }

    /*


    public Vector2i getArrayIndex(int x, int y)
    {
        Vector2i index = new Vector2i(0, 0);
        for (int i = 0; i < 4; i++)
        {
            if (x < i * xoffset)
            {
                index.X = i;
            }
        }

        for (int j = 0; j < 4; j++)
        {
            if (x < j * yoffset)
            {
                index.Y = j;
            }
        }

        return index;
    }
    */

}

