﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Stage
{
    private float lastTime = 0f;
    private float deltaTime = 0f;
    private bool PerformStageTransition = false;

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

    public void Initialize(Data d) { }
    public void Update(Data d) { }
    public void Finalize(Data d) { }

}