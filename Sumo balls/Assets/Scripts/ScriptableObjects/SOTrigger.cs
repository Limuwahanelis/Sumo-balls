using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="SOTrigger")]
public class SOTrigger : ScriptableObject
{
    public UnityEvent OnTriggered;

    public void Trigger()
    {
        OnTriggered?.Invoke();
    }
}
