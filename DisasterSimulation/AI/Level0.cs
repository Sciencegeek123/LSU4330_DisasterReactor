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
        public double[,] value = new double[4,4];


        public void Initialize(Level0 input, Image data)
        {
            // check if bigger than 256 x 256

            uint width = data.Size.X;
            uint height = data.Size.Y;


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
                    Grid[i,j] = new Image(width/4, height/4);
                    Rectangle temp = new Rectangle(0, 0, width / 4, height / 4);

                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, 104, 104), new Rectangle(i * 104, j * 104, 104, 104), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }

        }
    }
}
