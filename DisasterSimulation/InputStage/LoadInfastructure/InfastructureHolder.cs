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

    private Dictionary<uint, Vector2f> PointDictionary;
    private Dictionary<uint, uint> LinkDictionary;

    public void parseFile(string str)
    {

        List<uint> nodes = new List<uint>();
        XmlReader reader = XmlReader.Create(str);
        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "node"))
            {
                
                //This is to read in the road cooridinates
                if (reader.HasAttributes)
                {
                    
                    id = uint.Parse(reader.GetAttribute("id"));
                    nodes.Add(id);
                    latitude = float.Parse(reader.GetAttribute("lat"));
                    longitude = float.Parse(reader.GetAttribute("lon"));
                    Position.X = latitude;
                    Position.Y = longitude;
                    addPointToDictionary(id, Position);
                  
                }
               
                    
            }
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "way"))
            {
                if (reader.HasAttributes)
                {
                    reader.GetAttribute("id");
                }
            }
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "nd"))
                {
                    reader.Skip();
                }

                //This is to read in the road name
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "tag"))
                {
                    if (reader.HasAttributes && reader.GetAttribute("k") == "name")
                    {
                            Console.WriteLine("v= "+ reader.GetAttribute("v"));
                    // reader.GetAttribute("v");
                }
                else
                {
                    reader.Skip();
                }
            }
        }
        int i = nodes.Count - 1;
        while (i > 0)
        {
            addLinkToDictionary(nodes[i], nodes[i - 1]);
            i--;
        }
    }

    


   public void addPointToDictionary(uint ID, Vector2f Position)
   {
        id = ID;
        PointDictionary = new Dictionary<uint, Vector2f>();
        PointDictionary.Add(ID, Position);
        Console.WriteLine("Dictionary = " + Position);
        
   }
    
   public void addLinkToDictionary(uint ID, uint node)
   {
        id = ID;
        Node = node;
        LinkDictionary = new Dictionary<uint, uint>();
        LinkDictionary.Add(ID, node);
        Console.WriteLine("link Ditionary= " + ID +" " + node);
    }
    /*
   public Texture exportTextureResults()
   {

   }

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
