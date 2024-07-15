using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGameobjectDestroy : MonoBehaviour
{
    public UnityEvent OnGameobjectDestroyed;

    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnGameobjectDestroyed?.Invoke();
        OnGameobjectDestroyed.RemoveAllListeners();
    }
}
