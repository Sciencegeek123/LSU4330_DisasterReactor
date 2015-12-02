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
        
        XmlReader reader = XmlReader.Create(str);
        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "node"))
            {
                //This is to read in the road cooridinates
                if (reader.HasAttributes)
                {
                   reader.GetAttribute("id");
                   Console.WriteLine("lat= " + float.Parse(reader.GetAttribute("lat")));
                   Console.WriteLine("lon=" + float.Parse(reader.GetAttribute("lon")));
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
   */
   public bool isValid()
   {
        return true;

   }
   

}
