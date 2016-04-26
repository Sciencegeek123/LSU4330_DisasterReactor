using DisasterSimulation.OutputStage;
using SFML.Graphics;
using System.Collections.Generic;

partial class SimulationStage
{
    bool PrintResults(string filePath)
    {
        data.Graphics.RenderWindow();

        Image displayToSave = data.Graphics.ProgramDisplayTexture.Texture.CopyToImage();

        displayToSave.SaveToFile("SimulationDisplayOutput-1.png");

        List<string> Filepaths = new List<string>();
        List<string> Descriptions = new List<string>();

        Filepaths.Add("SimulationDisplayOutput-1.png");
        Descriptions.Add("Simulation Snapshot");

        print printer = new print();

        if(!printer.printfunction(Descriptions, Filepaths))
        {
            ErrorText.CurrentErrorText.ShowErrorText("Error creating pdf", 10f); // delta time triples during sim stage compared to input stage so need to multiply the value you want by 3
            return false;
        } 

        return true;

    }
}
