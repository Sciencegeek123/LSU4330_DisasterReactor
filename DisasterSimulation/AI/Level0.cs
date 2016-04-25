﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Collections;

namespace DisasterSimulation.AI
{
    class Level0
    {
        public Vector2f[,] Level0Value;
        private Color TC, EC;
        public Data data;
        public double xoffset;
        public double yoffset;

        public void Initialize(Image image, Data inputdata)
        {
            // check if bigger than 256 x 256

            double width = inputdata.Environment.Size.X;
            double height = inputdata.Environment.Size.Y;
            // Optional : Add the ability for user to change the number of partitions (increases / decreases computation )
            Level0Value = new Vector2f[4, 4];
            xoffset = Math.Ceiling(width/4);
            yoffset = Math.Ceiling(height/4);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Level0Value[i, j] = new Vector2f(0,0);
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
                    Level0Value[i, j] = calculateValue((int)(i *xoffset-1), (int)(j * yoffset - 1), Math.Ceiling(width), Math.Ceiling(height));
                }
            }

        }

        //xsize and ysize maybe useless.

        public Vector2f calculateValue(int xcoord, int ycoord, double xsize, double ysize)
        {
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
        }

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

    }
}
