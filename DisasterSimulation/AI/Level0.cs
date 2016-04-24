using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DisasterSimulation.AI
{
    class Level0
    {
        public Image[,] Grid = new Image[4, 4];
        public vector2u[,] value = new vector2u[4,4];
        public Image completeImage;
        private Color TC, EC;
        private Data data;

        public void Initialize(Image image, Data inputdata)
        {
            // check if bigger than 256 x 256

            uint width = image.Size.X;
            uint height = image.Size.Y;

            completeImage = image;
            data = inputdata;

            if (width < 1024 || height < 1024)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            //TODO may need to change number of splices (so it is a round number
            // pre defined sizes of grids
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    value[i, j] = totalValue(0, 0, 0, 0);

                    //offset depending on where it needs to be.

                    Grid[i,j] = new Image(width/4, height/4);
                    Rectangle temp = new Rectangle(0, 0, width / 4, height / 4);

                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, 104, 104), new Rectangle(i * 104, j * 104, 104, 104), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }

        }


        public float totalValue(uint xcoord, uint ycoord, uint xsize, uint ysize)
        {
            float totalaid = 0;
            double totalrepair = 0;


            for (uint x = xcoord; x < xcoord + xsize; x++)
            {
                for (uint y = ycoord; x < ycoord + ysize; y++)
                {
                    EC = data.Environment.GetPixel((x, y);
                    TC = completeImage.GetPixel(x, y);
                    totalaid += (data.rand.Next() % 512) * (EC.G - TC.G + 1) / (EC.B + 255);
                    totalrepair += (data.rand.Next() % 512) * (EC.R - TC.B + 1) / (EC.B + 255);
                }
            }

            return 0;

        }

    }
}
