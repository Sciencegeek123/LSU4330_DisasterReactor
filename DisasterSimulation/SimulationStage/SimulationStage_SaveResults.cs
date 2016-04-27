
partial class SimulationStage
{
    bool testVal = false;
    bool SaveResults(string filePath)
    {
        
        if(testVal)
        {
            // do stuff
            return true;
        }
        ErrorText.CurrentErrorText.ShowErrorText("Error saving file", 4.5f); // delta time triples during sim stage compared to input stage so need to multiply the value you want by 3
        return false;
    }
}
