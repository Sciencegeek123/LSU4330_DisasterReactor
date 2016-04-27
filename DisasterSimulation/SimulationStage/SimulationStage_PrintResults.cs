partial class SimulationStage
{
    bool testValue = false;
    bool PrintResults(string filePath)
    {
        if (testValue)
        {
            // do stuff
            return true;
        }
        ErrorText.CurrentErrorText.ShowErrorText("Error creating pdf", 4.5f); // delta time triples during sim stage compared to input stage so need to multiply the value you want by 3
        return false;
    }
}
