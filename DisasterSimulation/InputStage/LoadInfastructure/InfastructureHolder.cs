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

    
    public void parseFile(string str)
    {
        XmlReader reader = XmlReader.Create("C: \\Users\\Sam\\Downloads\\wcjones - disastersimulation - 6740de7a37db\\wcjones - disastersimulation - 6740de7a37db\\DisasterSimulation\\MapData.xml");
        Console.WriteLine();
        while (reader.Read())
        {
            Console.WriteLine();
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "node"))
            {
                Console.WriteLine();
                if (reader.HasAttributes)
                {
                    reader.GetAttribute("id");
                    float.Parse(reader.GetAttribute("lat"));
                    float.Parse(reader.GetAttribute("lon"));
                    Console.WriteLine();
                }
            }
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "way"))
            {
                if (reader.HasAttributes)
                {
                    reader.GetAttribute("id");
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "nd"))
                    {
                        reader.Skip();
                     //   if (reader.HasAttributes)
                     //   {
                     //       reader.GetAttribute("ref");
                     //   }
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "tag"))
                    {
                        if (reader.HasAttributes)
                        {
                            if (reader.GetAttribute("k") == "name")
                            {
                                reader.GetAttribute("k");
                                reader.GetAttribute("v");
                            }
                            else
                            {
                                reader.Skip();
                            }
                            
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /*

    public void addPointToDictionary(uint ID, Vector2f Position)
    {

    }

    public void addLinkToDictionary(uint ID, uint ID)
    {

    }

    public Texture exportTextureResults()
    {

    }

    public Vector2f getOrigin()
    {

    }

    public Vector2f getSize()
    {

    }

    public bool isValid()
    {

    }
    */

}
