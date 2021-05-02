using System;
using UnityEngine;

public abstract class RoutineBehaviour : MonoBehaviour
{
    public abstract int ExecutionPeriod { get; }
    private DateTime? lastExecution;

    public virtual void Update()
    {
        var now = DateTime.Now;
        if (!lastExecution.HasValue || lastExecution.Value.AddMilliseconds(ExecutionPeriod) < now)
        {
            ExecuteRoutine();
            lastExecution = now;
        }
    }

    public abstract void ExecuteRoutine();
}