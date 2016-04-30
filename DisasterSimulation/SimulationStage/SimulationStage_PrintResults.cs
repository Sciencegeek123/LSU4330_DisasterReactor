using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
/// <summary>
/// This file contains the code necessary for printing simulation output.
/// </summary>
partial class SimulationStage
{
    /// <summary>
    /// This function creates a single image based upon what the caller specifies is needed.
    /// </summary>
    /// <param name="img">The image to modify, passed by reference.</param>
    /// <param name="env">Render environment?</param>
    /// <param name="trails">Render trails?</param>
    /// <param name="spawn">Render spawns?</param>
    /// <param name="agents">Render agents?</param>
    void RenderImage(ref Image img, bool env, bool trails, bool spawn, bool agents)
    {
        RenderTexture tex = new RenderTexture(1024, 1024);

        tex.Clear(Color.Black);

        if (env)
        {
            tex.Draw(SEnv, new RenderStates(BlendMode.Add));
        }

        if (trails)
        {

            TTra.Update(data.Trails);
            tex.Draw(STra, new RenderStates(BlendMode.Add));
        }

        CircleShape template = new CircleShape();
        template.FillColor = Color.Black;
        template.OutlineColor = Color.White;
        template.OutlineThickness = 1;
        template.Radius = 4;
        template.Origin = new Vector2f(5, 5);
        if (spawn)
        {
            foreach (Vector2f point in data.SpawnPositions)
            {
                template.Position = point;
                tex.Draw(template);
            }
        }

        template.OutlineColor = Color.Black;

        template.OutlineThickness = 1;
        template.Radius = 2;
        template.Origin = new Vector2f(3, 3);

        if (agents)
        {
            foreach (Agent a in data.Agents)
            {
                template.Position = new Vector2f(a.Position.X, a.Position.Y);
                template.FillColor = a.info;
                tex.Draw(template);
            }
        }

        tex.Display();

        img = tex.Texture.CopyToImage();
    }

    /// <summary>
    /// This will generate a color given an integer and a max value. 0 LE value LE max
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    Color generateColor(int value, int max)
    {
        double val = Math.Log10(value) / Math.Log10(max);

        val *= 1536;

        byte c = (byte)((int)val % 256);

        if (val < 256)
        {
            return Color.Black;
        }
        else if (val < 512)
        {
            return new Color(c, 0, 0);
        }
        else if (val < 768)
        {
            return new Color(255, 0, c);
        }
        else if (val < 1024)
        {
            return new Color((byte)(255 - c), 0, 255);
        }
        else if (val < 1280)
        {

            return new Color(0, c, 255);
        }
        else if (val < 1536)
        {
            return new Color(0, 255, (byte)(255 - c));
        }
        else
        {
            return Color.White;
        }
    }

    /// <summary>
    /// This renders a heatmap given an input array of integers and an image to render to.
    /// </summary>
    /// <param name="img"></param>
    /// <param name="data"></param>
    void RenderHeatmap(ref Image img, int[,] data)
    {
        int maxVal = 0;
        foreach (int item in data)
        {
            if (item > maxVal)
            {
                maxVal = item;
            }
        }

        for (uint i = 0; i < 1024; i++)
        {
            for (uint j = 0; j < 1024; j++)
            {
                img.SetPixel(i, j, generateColor(data[i, j], maxVal));
            }
        }

    }

    /// <summary>
    /// This generates a number of images and saves them along with a PDF to a directory specified by the user.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    bool PrintResults(string filePath)
    {
        if (!filePath.EndsWith("/"))
            filePath += "/";

        Console.Out.WriteLine("Saving output to: " + filePath);

        Image displayToSave = new Image(1024, 1024);
        List<string> Filepaths = new List<string>();
        List<string> Descriptions = new List<string>();

        //Just Trails
        RenderImage(ref displayToSave, true, false, true, false);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-0.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-0.png");
        Descriptions.Add("Initial Conditions. Environment and Spawn");

        //All Values
        RenderImage(ref displayToSave, true, true, true, true);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-1.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-1.png");
        Descriptions.Add("Showing Environment, Trails, Spawns, and Agents");

        //No Agents
        RenderImage(ref displayToSave, true, true, true, false);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-2.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-2.png");
        Descriptions.Add("Showing Environment, Trails, and Spawns");

        //No Agents or spawns
        RenderImage(ref displayToSave, true, true, false, false);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-3.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-3.png");
        Descriptions.Add("Showing Environment and Trails");

        //Just Trails
        RenderImage(ref displayToSave, false, true, false, false);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-4.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-4.png");
        Descriptions.Add("Showing Trails");

        //Position Heatmap w/ Spawns
        RenderHeatmap(ref displayToSave, data.PositionHeatmap);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-5.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-5.png");
        Descriptions.Add("Showing Position Heatmap");

        //Repair Heatmap w/ Spawns
        RenderHeatmap(ref displayToSave, data.RepairHeatmap);
        displayToSave.SaveToFile(filePath + "SimulationDisplayOutput-6.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-6.png");
        Descriptions.Add("Showing Repair Heatmap");

        //Aid Heatmap w/ Spawns
        RenderHeatmap(ref displayToSave, data.AidHeatmap);
        displayToSave.SaveToFile(filePath+"SimulationDisplayOutput-7.png");
        Filepaths.Add(filePath + "SimulationDisplayOutput-7.png");
        Descriptions.Add("Showing Aid Heatmap");

        print printer = new print();

        if (!printer.printfunction(Descriptions, Filepaths, filePath))
        {
            ErrorText.CurrentErrorText.ShowErrorText("Error creating pdf", 10f); // delta time triples during sim stage compared to input stage so need to multiply the value you want by 3
            return false;
        }

        return true;

    }
}
