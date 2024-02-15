using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PowerUp : MonoBehaviour
{
    protected IObjectPool<PowerUp> _pool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPool(IObjectPool<PowerUp> pool) => _pool = pool;
    private void OnTriggerEnter(Collider other)
    {
        if(_pool!=null) _pool.Release(this);
        else Destroy(gameObject);

    }
}
