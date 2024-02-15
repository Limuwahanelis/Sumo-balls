using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TutorialTask : MonoBehaviour
{
    public UnityEvent OnTaskCompleted;
    public UnityEvent OnTaskStarted;


    public virtual void StartTask()
    {
        OnTaskStarted?.Invoke();
    }
    public virtual void CompleteTask()
    {
        OnTaskCompleted?.Invoke();
    }

}
