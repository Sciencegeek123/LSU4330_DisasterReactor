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

    float frameMagnitude;

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

    // input: image (with x, y, size coord 
    // output: vector2f with the calculations for each 

    public Vector2f calculateMagnitude(Vector2u position)
    {

        Vector2f magnitude = new Vector2f(0, 0);

        bool isInMyRegion = false; //You need to determine this.

        if (isInMyRegion)
        {
            //Look at every pixel in the region.
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

