using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


class Program
{
    static void Main(string[] args)
    {
        //Create everything
        Data data = new Data();
        InputStage inputControl = new InputStage();
        SimulationStage simControl = new SimulationStage();
        OutputStage outputControl = new OutputStage();

        //Perform initialization
        data.Initialize();

        //Input Program
        inputControl.Initialize(data);
        do
        {
            data.PreUpdate();
            inputControl.PreUpdate(data);
            inputControl.Update(data);
            data.PostUpdate();
        } while (!inputControl.transitionToNextStage());
        inputControl.Finalize(data);

        //Simulation Program
        simControl.Initialize(data);
        do
        {
            data.PreUpdate();
            simControl.PreUpdate(data);
            simControl.Update(data);
            data.PostUpdate();
        } while (!simControl.transitionToNextStage());
        simControl.Finalize(data);

        //Output Program
        outputControl.Initialize(data);
        do
        {
            data.PreUpdate();
            outputControl.PreUpdate(data);
            outputControl.Update(data);
            data.PostUpdate();
        } while (!simControl.transitionToNextStage());
        outputControl.Finalize(data);

    }
    
}

