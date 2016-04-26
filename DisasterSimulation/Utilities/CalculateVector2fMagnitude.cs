using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Utilities
{
    public static float CalculateVector2fMagnitude(Vector2f vec)
    {
        return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
    }
}

