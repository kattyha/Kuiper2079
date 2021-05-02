using System;
using UnityEngine;

public abstract class RoutineBehaviour : MonoBehaviour
{
    protected abstract int ExecutionPeriod { get; }
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

    protected abstract void ExecuteRoutine();
}