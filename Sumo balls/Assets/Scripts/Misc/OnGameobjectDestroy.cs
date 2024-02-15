using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGameobjectDestroy : MonoBehaviour
{
    public UnityEvent OnGameobjectDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        OnGameobjectDestroyed?.Invoke();
        OnGameobjectDestroyed.RemoveAllListeners();
    }
}
