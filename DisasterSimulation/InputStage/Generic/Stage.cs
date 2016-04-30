using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This is the base class for the Stages. It contains simple information, and a framework of functions.
/// 
/// Future developers can build off of this class.
/// </summary>
partial class Stage
{
    private float lastTime = 0f;
    private float deltaTime = 0f;
    protected bool PerformStageTransition = false;

    public bool transitionToNextStage()
    {
        return PerformStageTransition;
    }

    public float getDeltaTime()
    {
        return deltaTime;
    }

    public void PreUpdate(Data d)
    {

    }

    public virtual void Initialize(Data d) { }
    public virtual void Update() { }
    public virtual void Finalize(Data d) { }

}
