using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;


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

    private Vector2f OriginBounds, Size;

    private Dictionary<uint, Vector2f> PointDictionary = new Dictionary<uint, Vector2f>();
    private List<Tuple<uint, uint>> LinkList = new List<Tuple<uint, uint>>();

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
                Console.WriteLine("Found way");
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
    }




    public void addPointToDictionary(uint ID, Vector2f Position)
    {
        Position.X = (Position.X - OriginBounds.X) / Size.X;
        Position.Y = (Position.Y - OriginBounds.Y) / Size.Y;

        PointDictionary.Add(ID, Position);
        Console.WriteLine("Node: " + ID + " @ " + Position);

    }

    public void addLinkToDictionary(uint node1, uint node2)
    {
        LinkList.Add(new Tuple<uint, uint>(node1, node2));
        Console.WriteLine("Link: " + node1 + " - " + node2);
    }
    
   public Texture exportTextureResults()
   {
        RenderTexture Map = new RenderTexture(4096, 4096);
        Map.Clear(Color.Black);

        foreach (var link in LinkList)
        {
            //Draw a link by drawing a reactangle from one point to another with a thickness
            //Multiply the x and y coordinates by 4096.
            RectangleShape road = new RectangleShape();

            Map.Draw(road);
        }

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
    public bool isValid()
    {
        return true;

    }


}
