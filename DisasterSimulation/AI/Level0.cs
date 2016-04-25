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

        // check if bigger than 256 x 256
        /*
        double width = inputdata.Environment.Size.X;
        double height = inputdata.Environment.Size.Y;
        // Optional : Add the ability for user to change the number of partitions (increases / decreases computation )
        Level0Value = new Vector2f[4, 4];
        xoffset = Math.Ceiling(width / 4);
        yoffset = Math.Ceiling(height / 4);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Level0Value[i, j] = new Vector2f(0, 0);
            }
        }


        data = inputdata;

        // change this to whatever we want. 
        if (width < 1024 || height < 1024)
        {
            Console.WriteLine("P:");
            throw new System.ArgumentOutOfRangeException();
        }

        // pre defined sizes of grids
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //double check... that they are in the right order
                //Level0Value[i, j] = calculateValue((int)(i * xoffset - 1), (int)(j * yoffset - 1), Math.Ceiling(width), Math.Ceiling(height));
            }
        }
        */

    }

    //Calculate cached values for each iteration.
    public void Update()
    {

    }
    // check if position is within current commander bounds (look at offset)
    public Boolean isInMyRegion()
    {
        bool value = false;


        return value;
    }

    public Vector2f calculateMagnitude(Vector2u position)
    {
        
        Vector2f magnitude = new Vector2f(0, 0);

        bool isInMyRegion = false; //You need to determine this.

        if(isInMyRegion)
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
            Vector2f direction = new Vector2f(1, 1); //You need to determine;
            return direction * frameMagnitude;
        }


        return magnitude;
        /*
        float totalaid = 0;
        float totalrepair = 0;
        Vector2f returnvalue = new Vector2f();
        float count = 0;

        for (int x = getArrayIndex(xcoord,ycoord).X*(int)xoffset; x < (getArrayIndex(xcoord, ycoord).X+1) * (int)xoffset; x++)
        {
            for (int y = getArrayIndex(xcoord, ycoord).Y * (int)yoffset; y < (getArrayIndex(xcoord,ycoord).Y + 1) * (int)yoffset; y++)
            {
                EC = data.Environment.GetPixel((uint)x,(uint)y);
                TC = data.getPixel(x, y);
                totalaid += (data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
                totalrepair += (data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
                count++;
            }
        }

  
        returnvalue.X = totalaid;
        returnvalue.Y = totalrepair;
        return returnvalue / count ;
        */
    }

    /*
    public double getWidth()
    {
        return data.Environment.Size.X;
    }

    public double getHeight()
    {
        return data.Environment.Size.Y;
    }

    public Vector2i getArrayIndex(int x, int y)
    {
        Vector2i index = new Vector2i(0, 0);
        for(int i = 0; i < 4; i++)
        {
            if(x < i * xoffset)
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

