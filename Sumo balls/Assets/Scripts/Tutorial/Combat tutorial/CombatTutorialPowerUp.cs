using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatTutorialPowerUp : PowerUp
{
    public UnityEvent OnPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_pool != null)
        {
            _pool.Release(this);
            OnPickedUp?.Invoke();
        }
        else Destroy(gameObject);
    }
}
