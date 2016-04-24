using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DisasterSimulation.AI
{
    class Level0
    {
        public Image[,] Grid = new Image[4, 4];
        public Vector2f[,] value;
        public Image completeImage;
        private Color TC, EC;
        private Data data;

        public void Initialize(Image image, Data inputdata)
        {
            // check if bigger than 256 x 256

            double width = image.Size.X;
            double height = image.Size.Y;
            // Optional : Add the ability for user to change the number of partitions (increases / decreases computation )
            value = new Vector2f[4, 4];
            completeImage = image;
            data = inputdata;

            // change this to whatever we want. 
            if (width < 1024 || height < 1024)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            // pre defined sizes of grids
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //double check... that they are in the right order 
                    value[i, j] = calculateValue(0, 0, 0+i*Math.Ceiling(width), 0+j*Math.Ceiling(height));
                }
            }

        }


        public Vector2f calculateValue(uint xcoord, uint ycoord, double xsize, double ysize)
        {
            float totalaid = 0;
            float totalrepair = 0;
            Vector2f returnvalue = new Vector2f();

            for (uint x = xcoord; x < xcoord + xsize; x++)
            {
                for (uint y = ycoord; x < ycoord + ysize; y++)
                {
                    EC = data.Environment.GetPixel(x,y);
                    TC = completeImage.GetPixel(x, y);
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
