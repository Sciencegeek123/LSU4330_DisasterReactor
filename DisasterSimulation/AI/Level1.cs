using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisasterSimulation.AI
{


    class Level1
    {
        // create all the necessary variables
        public Vector2f[,] Level1Value;
        public void Initialize(Level0 input)
        {
            double width = input.data.Environment.Size.X;
            double height = input.data.Environment.Size.Y;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //double check... that they are in the right order 
                    Level1Value[i, j] = calculateValue(0, 0, 0 + i * Math.Ceiling(width), 0 + j * Math.Ceiling(height));
                }
            }

        }

        // input: image (with x, y, size coord 
        // output: vector2f with the calculations for each 

        public Vector2f calculateValue(uint xcoord, uint ycoord, double xsize, double ysize)
        {
            // should i be using average ?
            float totalaid = 0;
            float totalrepair = 0;
            Vector2f returnvalue = new Vector2f();
            Color TC, EC;

            for (uint x = xcoord; x < xcoord + xsize; x++)
            {
                for (uint y = ycoord; x < ycoord + ysize; y++)
                {
                    EC = data.Environment.GetPixel(x, y);
                    TC = data.Environment.GetPixel(x, y);
                    totalaid += (data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
                    totalrepair += (data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
                }
            }


            returnvalue.X = totalaid;
            returnvalue.Y = totalrepair;
            return returnvalue;
        }

    }
}
