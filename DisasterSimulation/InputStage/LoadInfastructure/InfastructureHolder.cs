using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

/// <summary>
/// Loads OSM files into the application.
/// </summary>
class InfastructureHolder
{
    /*
    Program flow.
    Call ParseFile
    Add all the points to the dictionary
    Connect all the points
    Export the Texture*/


    private float latitude;
    private float longitude;
    private uint id;
    private uint Node;
    private Vector2f Position;

    bool loaded = false;

    private Vector2f OriginBounds, Size;

    private Dictionary<uint, Vector2f> PointDictionary = new Dictionary<uint, Vector2f>();
    private List<Tuple<uint, uint>> LinkList = new List<Tuple<uint, uint>>();
    /// <summary>
    /// Reads an OSM file and stores it into a data structure.
    /// </summary>
    /// <param name="str">The filepath of the OSM file.</param>
    public void parseFile(string str)
    {
        XmlReader reader = XmlReader.Create(str);
        while (reader.Read())
        {
            if (reader.Name == "bounds")
            {
                latitude = (float)double.Parse(reader.GetAttribute("minlat"));
                longitude = (float)double.Parse(reader.GetAttribute("minlon"));
                OriginBounds = new Vector2f(latitude, longitude);
                latitude = (float)double.Parse(reader.GetAttribute("maxlat")) - latitude;
                longitude = (float)double.Parse(reader.GetAttribute("maxlon")) - longitude;
                Size = new Vector2f(latitude, longitude);

            }
            else if (reader.Name == "node")
            {

                //This is to read in the road cooridinates
                if (reader.HasAttributes)
                {

                    id = uint.Parse(reader.GetAttribute("id"));
                    latitude = float.Parse(reader.GetAttribute("lat"));
                    longitude = float.Parse(reader.GetAttribute("lon"));
                    Position.X = latitude;
                    Position.Y = longitude;
                    addPointToDictionary(id, Position);

                }


            }
            else if (reader.Name == "way")
            {
               // Console.WriteLine("Found way");
                XmlReader childReader = reader.ReadSubtree();
                childReader.ReadToDescendant("nd");
                uint id1 = 0, id2 = 0;
                do
                {
                    if (childReader.Name == "nd")
                    {
                        id2 = uint.Parse(childReader.GetAttribute("ref"));
                        if (id1 != 0)
                        {
                            addLinkToDictionary(id1, id2);
                        }
                        id1 = id2;
                    }
                } while (childReader.Read());


            }
        }
        Console.Out.WriteLine("Map loaded.");
        loaded = true;
    }

    /// <summary>
    /// Adds a node from the OSM file being read to a dictionary.
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Position"></param>
    public void addPointToDictionary(uint ID, Vector2f Position)
    {
        Position.X = (Position.X - OriginBounds.X) / Size.X;
        Position.Y = (Position.Y - OriginBounds.Y) / Size.Y;

        PointDictionary.Add(ID, Position);
       // Console.WriteLine("Node: " + ID + " @ " + Position);

    }

    /// <summary>
    /// Adds an edge from the OSM file benig read to a list.
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    public void addLinkToDictionary(uint node1, uint node2)
    {
        LinkList.Add(new Tuple<uint, uint>(node1, node2));
      //  Console.WriteLine("Link: " + node1 + " - " + node2);
    }

    /// <summary>
    /// Calculates the distance between two points.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public float Dist(Vector2f a, Vector2f b)
    {
        return (float)Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
    }
    
    /// <summary>
    /// Produces a value from the loaded data.
    /// </summary>
    /// <returns></returns>
   public Texture exportTextureResults()
   {
        RenderTexture Map = new RenderTexture(1024, 1024);
        Map.Clear(new Color(32, 32, 196));

        Vertex[] Corners = new Vertex[4];

        for (int i = 0; i < 4; i++)
            Corners[i].Color = new Color(16, 16, 32);

        foreach (var link in LinkList)
        {
            //Draw a link by drawing a reactangle from one point to another with a thickness
            //Multiply the x and y coordinates by 1024.
            Vector2f Node1, Node2;

            if(!PointDictionary.TryGetValue(link.Item1, out Node1))
                continue;
            if (!PointDictionary.TryGetValue(link.Item2, out Node2))
                continue;

            Node1 *= 1024;
            Node2 *= 1024;

            Vector2f Dir = (Node2 - Node1) / Dist(Node1, Node2);
            Dir = new Vector2f(Dir.Y, Dir.X);

            Vector2f Thickness = 2 * Dir;

            Corners[0].Position = Node1 + Thickness;
            Corners[1].Position = Node1 - Thickness;
            Corners[2].Position = Node2 + Thickness;
            Corners[3].Position = Node2 - Thickness;

            Map.Draw(Corners, PrimitiveType.Quads);
        }

        Map.Display();

        return Map.Texture;
   }

    /*
   public Vector2f getOrigin()
   {

   }

   public Vector2f getSize()
   {

   }
   */

    /// <summary>
    /// Returns true if the file has been loaded correctly.
    /// </summary>
    /// <returns></returns>
    public bool isValid()
    {
        return loaded;

    }


}
