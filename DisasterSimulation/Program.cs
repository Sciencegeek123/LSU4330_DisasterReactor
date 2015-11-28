using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace DisasterSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(1536, 1024), "CS3380 - Disaster Recovery - W. Jones & S. Shrestha");
            window.SetActive();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Display();
            }
        }
    
    }
}
